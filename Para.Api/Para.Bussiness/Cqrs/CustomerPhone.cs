using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs
{
    public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
    public record UpdateCustomerPhoneCommmand(long CustomerId, CustomerPhoneRequest Request) : IRequest<ApiResponse>;
    public record DeleteCustomerPhoneCommmand(long CustomerId) : IRequest<ApiResponse>;


    public record GetAllCustomerPhoneQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
    public record GetAllCustomerPhoneByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerPhoneResponse>>;
    public record GetCustomerPhoneByParametersQuery(long CustomerId) : IRequest<ApiResponse<CustomerPhoneResponse>>;
}
