using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Vennderful.Application.Features.User.Requests;
using Vennderful.Application.Common;
using Vennderful.Identity.Interfaces;
using Vennderful.Application.Features.User.DTOs;
using MediatR;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Identity.Model;
using Vennderful.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Vennderful.API.Extensions;
using System.IdentityModel.Tokens.Jwt;
using Vennderful.Application.Features.UserRoles.Requests;

namespace Vennderful.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;
        private readonly ILogger _logger;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService<ApplicationUser> _tokenService;



        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ITokenService<ApplicationUser> tokenService, ILogger<AuthController> logger)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<CreateUserResponse>> Register([FromBody] UserRegisterDto userProfile)
        {

            BaseResponse result = new BaseResponse();

            var email = userProfile.Email.ToLower();

            if (userProfile == null || !ModelState.IsValid)
            {
                result.ResponseStatus = ResponseStatus.Error;
                result.Message = "Email parameter is Empty";
                return BadRequest(result);
            }

            if (await UserExists(email))
            {
                result.ResponseStatus = ResponseStatus.Error;
                result.Message = "That email address is taken. Try another.";

                return Conflict(result);
            }

            ApplicationUser user = new ApplicationUser();

            user.Email = email;
            user.PasswordHash = userProfile.Password;
            user.UserName = user.Id;

            var reg = await _userManager.CreateAsync(user, userProfile.Password);

            if (!reg.Succeeded)
            {
                result.ResponseStatus = ResponseStatus.Error;
                result.Message = "Weak Password";

                return BadRequest(result);
            }



            if (!reg.Succeeded)
            {
                result.ResponseStatus = ResponseStatus.Error;
                result.Message = "Something went wrong";

                return BadRequest(result);
            }

         
            var token = await _tokenService.CreateTokenAsync(user);

            userProfile.UserId = Guid.Parse(user.Id);
            var command = new CreateUserCommand { UserRegisterDto = userProfile };
            CreateUserResponse userResult = await Mediator.Send(command);

            // update the token with companyId
            if (userResult != null && userResult.Data != null)
            {
                token = await UpdateRegToken(token, userResult.Data.CompanyId.ToString(), userResult.Data.FirstName, userResult.Data.LastName, userResult.Data.FirstName);
            }

            result.Data = token;
            result.Message = "Registered Successfully";
            result.ResponseStatus = ResponseStatus.Success;

            return Ok(result);

        }

        [HttpPost("signin")]
        public async Task<ActionResult> Signin([FromBody] UserLoginDto userProfile)
        {
            BaseResponse result = new BaseResponse();
            ApplicationUser existingUser = await _userManager.FindByEmailAsync(userProfile.Email);

            if (existingUser == null)
            {
                result.ResponseStatus = ResponseStatus.Error;
                result.Message = "Couldn't find your Vennderful Account";

                return Unauthorized(result);
            }

            var check = await _signInManager.CheckPasswordSignInAsync(existingUser, userProfile.Password, false);
            if (!check.Succeeded)
            {
                result.ResponseStatus = ResponseStatus.Error;
                result.Message = "Wrong password. Try again.";

                return Unauthorized(result);

            }
            var token = await _tokenService.CreateTokenAsync(existingUser);

            var request = new GetCurrentUser { IdentityId = Guid.Parse(existingUser.Id) };
            GetCurrentUserResponse existingUserProfile = await Mediator.Send(request);
            var userRole = GetUserRole(existingUserProfile.Data.CompanyId);
            // update the token with companyId
            if (existingUserProfile != null && existingUserProfile.Data != null)
            {
                token = await UpdateToken(token, existingUserProfile.Data.CompanyId.ToString(), existingUserProfile.Data.FirstName, existingUserProfile.Data.LastName, existingUserProfile.Data.FirstName, await userRole, existingUserProfile.Data.UserId.ToString());
            }

            result.Data = token;
            result.Success = true;
            result.ResponseStatus = ResponseStatus.Success;
            result.Message = "Successfully loged in";

            return Ok(result);
        }

        [HttpGet("GetCurrentUser/{Guid}")]
        public async Task<ActionResult<UserProfile>> Get(Guid Guid)
        {
            var user = await Mediator.Send(new GetCurrentUser { IdentityId = Guid });

            return Ok(user);
        }

        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserProfile>> Get()
        {
            var user = ClaimsPrincipalExtension.GetUsername(User);
            var currentUser = await Mediator.Send(new GetCurrentUser { IdentityId = Guid.Parse(user) });

            return Ok(currentUser);
        }

        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest(new BaseResponse { Message = "Invalid Email Confirmation Request" });
            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest(new BaseResponse { Message = "Invalid Email Confirmation Request" });
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Google-login")]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Auth");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Signin));
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
                return Ok(userInfo);
            else
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };
                IdentityResult identResult = await _userManager.CreateAsync(user);


                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return Ok(userInfo);
                    }
                }
                return BadRequest();
            }
        }


        [HttpPost("Register_Google_User")]
        public async Task<ActionResult<CreateUserResponse>> RegisterGoogleUser([FromBody] UserRegisterDto userProfile)
        {

            BaseResponse result = new BaseResponse();

            ApplicationUser user = new ApplicationUser();
            UserProfileDTO profile;

            var email = userProfile.Email.ToLower();

            if (userProfile == null || !ModelState.IsValid)
            {
                result.ResponseStatus = ResponseStatus.Error;
                result.Message = "Email parameter is Empty";
                return BadRequest(result);
            }

            if (await UserExists(userProfile.Email))
            {
                user = await _userManager.FindByEmailAsync(userProfile.Email);
                var request = new GetCurrentUser { IdentityId = Guid.Parse(user.Id) };
                GetCurrentUserResponse userResult = await Mediator.Send(request);
                if (userResult == null || userResult.Data == null)
                {
                    result.ResponseStatus = ResponseStatus.Info;
                    result.Message = "This email address is not found under users list.";

                    return Ok(result);
                }

                profile = userResult.Data;

                var currentUser = new GetCurrentUser { IdentityId = Guid.Parse(user.Id) };
                GetCurrentUserResponse existingUserProfile = await Mediator.Send(currentUser);

                var token = await _tokenService.CreateTokenAsync(user);
                var userRole = GetUserRole(existingUserProfile.Data.CompanyId);
                // update the token with companyId
                if (profile != null)
                {
                    token = await UpdateToken(token, profile.CompanyId.ToString(), profile.FirstName, profile.LastName, profile.FirstName, await userRole, existingUserProfile.Data.UserId.ToString()); 
                }


                result.Data = token;
                result.Message = "User Exists";
                result.ResponseStatus = ResponseStatus.Success;

                return Ok(result);
            }
            else
            {
                user.Email = email;
                user.PasswordHash = "@TestPa$s5";
                user.UserName = user.Id;

                var reg = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!reg.Succeeded)
                {
                    result.ResponseStatus = ResponseStatus.Error;
                    result.Message = "Something went wrong while registering user";

                    return BadRequest(result);
                }
                userProfile.UserId = Guid.Parse(user.Id);
                var command = new CreateUserCommand { UserRegisterDto = userProfile };
                CreateUserResponse userResult = await Mediator.Send(command);
                profile = userResult.Data;

                var token = await _tokenService.CreateTokenAsync(user);
                // update the token with companyId
                if (profile != null)
                {
                    token = await UpdateRegToken(token, profile.CompanyId.ToString(), profile.FirstName, profile.LastName, profile.FirstName);
                }


                result.Data = token;
                result.Message = "Registered Successfully";
                result.ResponseStatus = ResponseStatus.Success;

                return Ok(result);
            }            

           

        }
        public async Task<string> GetUserRole(Guid id)
        {
            var user = await Mediator.Send(new GetRoleByUserQuery { CompanyId = id });
            if (user.Data == null)
            {
                return "Empty";
            }
                
            else 
            {
                return user.Data.UserRoleType.ToString();
            }

            
        }

        [AllowAnonymous]
        [HttpPost("SignInWithFacebook")]
        public IActionResult SignInWithFacebook()
        {
            var authenticationProperties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action(nameof(HandleExternalLogin)));
            return Challenge(authenticationProperties, "Facebook");
        }

        public async Task<IActionResult> HandleExternalLogin()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (!result.Succeeded) //user does not exist yet
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var newUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = false
                };
                var createResult = await _userManager.CreateAsync(newUser);
                if (!createResult.Succeeded)
                    throw new Exception(createResult.Errors.Select(e => e.Description).Aggregate((errors, error) => $"{errors}, {error}"));

                await _userManager.AddLoginAsync(newUser, info);
                var newUserClaims = info.Principal.Claims.Append(new Claim("userId", newUser.Id));
                await _userManager.AddClaimsAsync(newUser, newUserClaims);
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }

            return Redirect("https://venderfullqa.com/signup");
        }

        [HttpPost("Register_Facebook_User")]
        public async Task<ActionResult<CreateUserResponse>> RegisterFacebookUser([FromBody] UserRegisterDto userProfile)
        {

            BaseResponse result = new BaseResponse();
            ApplicationUser user = new ApplicationUser();
            UserProfileDTO profile;

            var email = userProfile.Email.ToLower();

            if (userProfile == null || !ModelState.IsValid)
            {
                result.ResponseStatus = ResponseStatus.Error;
                result.Message = "Email parameter is Empty";
                return BadRequest(result);
            }

            if (await UserExists(userProfile.Email))
            {
                user = await _userManager.FindByEmailAsync(userProfile.Email);
                var request = new GetCurrentUser { IdentityId = Guid.Parse(user.Id) };
                GetCurrentUserResponse userResult = await Mediator.Send(request);
                if (userResult == null || userResult.Data == null)
                {
                    result.ResponseStatus = ResponseStatus.Info;
                    result.Message = "This email address is not found under users list.";

                    return Ok(result);
                }


                profile = userResult.Data;

                var currentUser = new GetCurrentUser { IdentityId = Guid.Parse(user.Id) };
                GetCurrentUserResponse existingUserProfile = await Mediator.Send(currentUser);


                var token = await _tokenService.CreateTokenAsync(user);
                var userRole = GetUserRole(existingUserProfile.Data.CompanyId);
                // update the token with companyId
                if (profile != null)
                {
                    token = await UpdateToken(token, profile.CompanyId.ToString(), profile.FirstName, profile.LastName, profile.FirstName, await userRole, existingUserProfile.Data.UserId.ToString());
                }


                result.Data = token;
                result.Message = "User Exists";
                result.ResponseStatus = ResponseStatus.Success;

                return Ok(result);
            }
            else
            {
                user.Email = email;
                user.PasswordHash = "@TestPa$s5";
                user.UserName = user.Id;

                var reg = await _userManager.CreateAsync(user, user.PasswordHash);

                if (!reg.Succeeded)
                {
                    result.ResponseStatus = ResponseStatus.Error;
                    result.Message = "Something went wrong while registering user";

                    return BadRequest(result);
                }

                userProfile.UserId = Guid.Parse(user.Id);
                var command = new CreateUserCommand { UserRegisterDto = userProfile };
                CreateUserResponse userResult = await Mediator.Send(command);
                profile = userResult.Data;

                var token = await _tokenService.CreateTokenAsync(user);
                // update the token with companyId
                if (profile != null)
                {
                    token = await UpdateRegToken(token, profile.CompanyId.ToString(), profile.FirstName, profile.LastName, profile.FirstName);
                }


                result.Data = token;
                result.Message = "Registered Successfully";
                result.ResponseStatus = ResponseStatus.Success;

                return Ok(result);

            }              

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("https://venderfullqa.com");
        }

        private async Task<bool> UserExists(string email)
        {

            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        private async Task<string> UpdateToken(string token, string CompanyId, string fname, string lname, string image, string? userRole, string? userId)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken existingToken = tokenHandler.ReadJwtToken(token.ToString());
                var newClaim_Id = new Claim("companyId", CompanyId.ToString());
                var newClaim_Name = new Claim("name", fname.ToString() + " " +lname.ToString());
                var newClaim_Image = new Claim("image", image.ToString());
                var newClaim_UserRole = new Claim("UserRole", userRole.ToString() ?? "Empty"); 
                var newClaim_UserId = new Claim("UserId", userId.ToString());
                List<Claim> existingClaims = existingToken.Claims.ToList();
                existingClaims.Add(newClaim_Id);
                existingClaims.Add(newClaim_Name);
                existingClaims.Add(newClaim_Image);
                existingClaims.Add(newClaim_UserRole);
                existingClaims.Add(newClaim_UserId);
                JwtSecurityToken newToken = new JwtSecurityToken(
                    issuer: existingToken.Issuer,
                    claims: existingClaims,
                    expires: existingToken.ValidTo);
                token = new JwtSecurityTokenHandler().WriteToken(newToken);
                return token.ToString();
            }
            catch (Exception ex) { return string.Empty;  }
        }

        private async Task<string> UpdateRegToken(string token, string CompanyId, string fname, string lname, string image)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken existingToken = tokenHandler.ReadJwtToken(token.ToString());
                var newClaim_Id = new Claim("companyId", CompanyId.ToString());
                var newClaim_Name = new Claim("name", fname.ToString() + " " + lname.ToString());
                var newClaim_Image = new Claim("image", image.ToString());
                List<Claim> existingClaims = existingToken.Claims.ToList();
                existingClaims.Add(newClaim_Id);
                existingClaims.Add(newClaim_Name);
                existingClaims.Add(newClaim_Image);
                JwtSecurityToken newToken = new JwtSecurityToken(
                    issuer: existingToken.Issuer,
                    claims: existingClaims,
                    expires: existingToken.ValidTo);
                token = new JwtSecurityTokenHandler().WriteToken(newToken);
                return token.ToString();
            }
            catch (Exception ex) { return string.Empty; }
        }
        private class LoginModel
        {
            public string ? ReturnUrl;
            public List<Microsoft.AspNetCore.Authentication.AuthenticationScheme> ? ExternalLogins;

        }
    }
}

