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
    public class CustomerDetailController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CustomerDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> Get()
        {
            var operation = new GetAllCustomerDetailQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> Get([FromRoute] long customerId)
        {
            var operation = new GetAllCustomerDetailByIdQuery(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidatorAttribute))]
        public async Task<ApiResponse<CustomerDetailResponse>> Post([FromBody] CustomerDetailRequest value)
        {
            var operation = new CreateCustomerDetailCommand(value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerId}")]
        [ServiceFilter(typeof(ValidatorAttribute))]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerDetailRequest value)
        {
            var operation = new UpdateCustomerDetailCommmand(customerId, value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerDetailCommmand(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

    }
}
