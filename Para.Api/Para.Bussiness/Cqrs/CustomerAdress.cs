using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerAdressCommmand(CustomerAddressRequest Request) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record UpdateCustomerAdressCommmand(long CustomerId, CustomerAddressRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerAdressCommmand(long CustomerId) : IRequest<ApiResponse>;


public record GetAllCustomerAdressQuery() : IRequest<ApiResponse<List<CustomerAddressResponse>>>;
public record GetAllCustomerAdressByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerAddressResponse>>;
public record GetCustomerDefaultAdresssByParametersQuery(long CustomerId) : IRequest<ApiResponse<CustomerAddressResponse>>;