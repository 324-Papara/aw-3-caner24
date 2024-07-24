using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerDetailCommand(CustomerDetailRequest Request) : IRequest<ApiResponse<CustomerDetailResponse>>;
public record UpdateCustomerDetailCommmand(long CustomerId, CustomerDetailRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerDetailCommmand(long CustomerId) : IRequest<ApiResponse>;


public record GetAllCustomerDetailQuery() : IRequest<ApiResponse<List<CustomerDetailResponse>>>;
public record GetAllCustomerDetailByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerDetailResponse>>;
public record GetCustomerDetailByParametersQuery(long CustomerId) : IRequest<ApiResponse<CustomerDetailResponse>>;