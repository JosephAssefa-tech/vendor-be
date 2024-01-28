
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Contracts.Infrastructure.Mail;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Models.Mail;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;

        public MailController(IMediator mediator, IEmailService emailService)
        {
            _mediator = mediator;
            _emailService = emailService;
        }

        [HttpPost("{companyId}/SendEmail", Name = "SendEmail")]
        public async Task<ActionResult<CreateCustomerResponse>> SendEmail([FromBody] Email email)
        {
            var command = new CreateCustomerCommand { email = email };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

    }
}
