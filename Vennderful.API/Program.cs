using Microsoft.AspNetCore.Authentication.JwtBearer;



using Microsoft.AspNetCore.Identity;



using Microsoft.EntityFrameworkCore;



using Microsoft.IdentityModel.Tokens;



using System.Text;



using Vennderful.Application;



using Vennderful.Application.Contracts.Payment;



using Vennderful.Domain.Entities;



using Vennderful.Identity;



using Vennderful.Persistence;



using Vennderful.Infrastructure;



using Vennderful.Persistence.Contexts;



using Stripe;



using Vennderful.Payment;



using Microsoft.OpenApi.Models;
using BlobStorage;

var builder = WebApplication.CreateBuilder(args);



var configuration = builder.Configuration;






builder.Services.AddDbContext<VennderfulDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("VennderfulDB")));



builder.Services.AddDbContext<VennderfulIdentityDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("Vennderful_IdentityDB")));







// Add services to the container.






// Dependency Services



builder.Services.AddApplicationServices();



builder.Services.AddBlobStorageServices(builder.Configuration);


builder.Services.AddPersistenceServices(builder.Configuration);



builder.Services.AddInfrastructureIdentityServices(builder.Configuration);



builder.Services.AddInfrastructureServices(builder.Configuration);



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)



.AddJwtBearer(options =>



{



    options.TokenValidationParameters = new TokenValidationParameters



    {



        ValidateIssuerSigningKey = true,



        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),



        ValidateIssuer = false,



        ValidateAudience = false,



    };






}).AddGoogle(googleOption =>



{



    var iDProperty = builder.Configuration.GetSection("Google");



    if (iDProperty == null)



    {



        throw new Exception("Failed to find Google Parameter Values");



    }



    googleOption.ClientId = builder.Configuration["Google:ClientId"];



    googleOption.ClientSecret = builder.Configuration["Google:ClientSecret"];



    googleOption.SignInScheme = IdentityConstants.ExternalScheme;



}).AddFacebook(fBOptions =>



{



    fBOptions.AppId = builder.Configuration["Facebook:AppId"];



    fBOptions.AppSecret = builder.Configuration["Facebook:AppSecret"];



});







builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(c => {



    c.SwaggerDoc("v1", new OpenApiInfo



    {



        Title = "JWTToken_Auth_API",



        Version = "v1"



    });



    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()



    {



        Name = "Authorization",



        Type = SecuritySchemeType.ApiKey,



        Scheme = "Bearer",



        BearerFormat = "JWT",



        In = ParameterLocation.Header,



        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",



    });



    c.AddSecurityRequirement(new OpenApiSecurityRequirement {



{



new OpenApiSecurityScheme {



Reference = new OpenApiReference {



Type = ReferenceType.SecurityScheme,



Id = "Bearer"



}



},



new string[] {}



}



});



});



builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddEndpointsApiExplorer();







builder.Services.Configure<IdentityOptions>(options =>



{



    // Default Lockout settings.



    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);



    options.Lockout.MaxFailedAccessAttempts = 5;



    options.Lockout.AllowedForNewUsers = true;



});







// Add Stripe Infrastructure



builder.Services.AddStripeInfrastructure(builder.Configuration);






//CORS



builder.Services.AddCors(options =>






options.AddPolicy("OpenPolicy", builder => builder



.AllowAnyOrigin()



.AllowAnyHeader()



.AllowAnyMethod())






);






var app = builder.Build();






//using (var scope = app.Services.CreateScope())



//{



//    var db = scope.ServiceProvider.GetRequiredService<VennderfulDbContext>();



//    db.Database.Migrate();



//}






// create a logger factory



var loggerFactory = LoggerFactory.Create(



builder => builder



.AddConsole()



.AddDebug()



.SetMinimumLevel(LogLevel.Debug)



);






// create a logger



var logger = loggerFactory.CreateLogger<Program>();






// logging



logger.LogTrace("Trace message");



logger.LogDebug("Debug message");



logger.LogInformation("Info message");



logger.LogWarning("Warning message");



logger.LogError("Error message");



logger.LogCritical("Critical message");








// Configure the HTTP request pipeline.






if (app.Environment.IsDevelopment())



{



    app.UseDeveloperExceptionPage();



    app.UseSwagger();



    app.UseSwaggerUI();



    app.UseCors("OpenPolicy");



}



else if (app.Environment.IsProduction())



{



    app.UseSwagger(); app.UseSwaggerUI();



    app.UseCors("OpenPolicy");



}






app.UseHttpsRedirection();






app.UseAuthorization();






app.UseCors("OpenPolicy");






app.MapControllers();






app.Run();