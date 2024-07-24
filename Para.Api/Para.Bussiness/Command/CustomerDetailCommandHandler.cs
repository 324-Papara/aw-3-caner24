using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Command
{
    public class CustomerDetailCommandHandler :
    IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>,
    IRequestHandler<UpdateCustomerDetailCommmand, ApiResponse>,
    IRequestHandler<DeleteCustomerDetailCommmand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteCustomerDetailCommmand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CustomerDetailRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(UpdateCustomerDetailCommmand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            var customer = await unitOfWork.CustomerRepository.GetById(request.CustomerId);
            if (customer == null)
                throw new Exception("Customer mevcut değil !");
            mapped.Customer = customer;
            unitOfWork.CustomerDetailRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
            mapped.CustomerId = mapped.CustomerId;
            await unitOfWork.CustomerDetailRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerDetailResponse>(mapped);
            return new ApiResponse<CustomerDetailResponse>(response);
        }
    }
}
