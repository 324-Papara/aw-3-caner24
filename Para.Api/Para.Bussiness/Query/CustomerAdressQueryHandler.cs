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

namespace Para.Bussiness.Query
{
    public class CustomerAdressQueryHandler :
    IRequestHandler<GetAllCustomerAdressQuery, ApiResponse<List<CustomerAddressResponse>>>,
    IRequestHandler<GetAllCustomerAdressByIdQuery, ApiResponse<CustomerAddressResponse>>,
    IRequestHandler<GetCustomerDefaultAdresssByParametersQuery, ApiResponse<CustomerAddressResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerAdressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerAddressResponse>>> Handle(GetAllCustomerAdressQuery request, CancellationToken cancellationToken)
        {
            List<CustomerAddress> entityList = await unitOfWork.CustomerAddressRepository.GetAll();
            var mappedList = mapper.Map<List<CustomerAddressResponse>>(entityList);
            return new ApiResponse<List<CustomerAddressResponse>>(mappedList);
        }

        public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetAllCustomerAdressByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CustomerAddressRepository.GetById(request.CustomerId);
            var mapped = mapper.Map<CustomerAddressResponse>(entity);
            return new ApiResponse<CustomerAddressResponse>(mapped);
        }

        public async Task<ApiResponse<CustomerAddressResponse>> Handle(GetCustomerDefaultAdresssByParametersQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CustomerAddressRepository.GetAll();
            entity.Where(x => x.CustomerId == request.CustomerId && x.IsDefault == true).FirstOrDefault();
            var mapped = mapper.Map<CustomerAddressResponse>(entity);
            return new ApiResponse<CustomerAddressResponse>(mapped);
        }
    }
}
