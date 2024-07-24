using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Api.Attribute;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPhoneController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CustomerPhoneController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Get()
        {
            var operation = new GetAllCustomerPhoneQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> Get([FromRoute] long customerId)
        {
            var operation = new GetAllCustomerPhoneByIdQuery(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidatorAttribute))]
        public async Task<ApiResponse<CustomerPhoneResponse>> Post([FromBody] CustomerPhoneRequest value)
        {
            var operation = new CreateCustomerPhoneCommand(value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerId}")]
        [ServiceFilter(typeof(ValidatorAttribute))]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerPhoneRequest value)
        {
            var operation = new UpdateCustomerPhoneCommmand(customerId, value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerPhoneCommmand(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
