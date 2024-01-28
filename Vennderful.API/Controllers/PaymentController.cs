
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.Stripe.DTOs;
using Vennderful.Application.Features.Stripe.Requests;
using Vennderful.Application.Features.Stripe.Responses;

namespace Vennderful.API.Controllers
{
    [Route("")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{companyId}/payment/stripe-customer")]
        public async Task<ActionResult<AddStripeCustomerResponse>> AddStripeCustomer(
            [FromBody] AddStripeCustomerDTO stripeCustomerDto, string companyId,
            CancellationToken ct)
        {
            var command = new AddStripeCustomerCommand { AddStripeCustomerDTO = stripeCustomerDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/stripe/{result.Data.Id}", UriKind.Relative),
                result.Data);
        }

        [HttpPost("{companyId}/payment/stripe-payment")]
        public async Task<ActionResult<AddStripePaymentResponse>> AddStripePayment(
            [FromBody] AddStripePaymentDTO stripePaymentDto, string companyId,
            CancellationToken ct)
        {
            stripePaymentDto.CompanyId = Guid.Parse(companyId);
            var command = new AddStripePaymentCommand { AddStripePaymentDTO = stripePaymentDto };
            var result = await _mediator.Send(command);

            if (result.Errors != null && result.Errors.Count() > 0)
                return BadRequest(result);
            return Created(new Uri($"/stripe/{result.Data.Id}", UriKind.Relative),
                result.Data);
        }
    }
}
