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
    public class CustomerAdressComandHandler : IRequestHandler<CreateCustomerAdressCommmand, ApiResponse<CustomerAddressResponse>>,
        IRequestHandler<UpdateCustomerAdressCommmand, ApiResponse>,
         IRequestHandler<DeleteCustomerAdressCommmand, ApiResponse>

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerAdressComandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerAddressResponse>> Handle(CreateCustomerAdressCommmand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            var customer = await unitOfWork.CustomerRepository.GetById(request.Request.CustomerId);
            if (customer == null)
                throw new Exception("Customer mevcut değil !");
            mapped.Customer = customer;
            await unitOfWork.CustomerAddressRepository.Insert(mapped);
            await unitOfWork.Complete();

            var response = mapper.Map<CustomerAddressResponse>(mapped);
            return new ApiResponse<CustomerAddressResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateCustomerAdressCommmand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<CustomerAddressRequest, CustomerAddress>(request.Request);
            var customer = await unitOfWork.CustomerRepository.GetById(request.CustomerId);
            if (customer == null)
                throw new Exception("Customer mevcut değil !");
            mapped.Customer = customer;
            unitOfWork.CustomerAddressRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteCustomerAdressCommmand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CustomerAddressRepository.Delete(request.CustomerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}
