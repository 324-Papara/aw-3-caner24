using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Para.Api.Attribute;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Get()
        {
            var operation = new GetAllCustomerAdressQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> Get([FromRoute] long customerId)
        {
            var operation = new GetAllCustomerAdressByIdQuery(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidatorAttribute))]
        public async Task<ApiResponse<CustomerAddressResponse>> Post([FromBody] CustomerAddressRequest value)
        {
            var operation = new CreateCustomerAdressCommmand(value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerId}")]
        [ServiceFilter(typeof(ValidatorAttribute))]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerAddressRequest value)
        {
            var operation = new UpdateCustomerAdressCommmand(customerId, value);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            var operation = new DeleteCustomerAdressCommmand(customerId);
            var result = await _mediator.Send(operation);
            return result;
        }


    }
}
