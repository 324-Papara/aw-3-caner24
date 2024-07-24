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
    public class CustomerPhoneQueryHandler :
    IRequestHandler<GetAllCustomerPhoneQuery, ApiResponse<List<CustomerPhoneResponse>>>,
    IRequestHandler<GetAllCustomerPhoneByIdQuery, ApiResponse<CustomerPhoneResponse>>,
    IRequestHandler<GetCustomerPhoneByParametersQuery, ApiResponse<CustomerPhoneResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerPhoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> Handle(GetAllCustomerPhoneQuery request, CancellationToken cancellationToken)
        {
            List<CustomerPhone> entityList = await unitOfWork.CustomerPhoneRepository.GetAll();
            var mappedList = mapper.Map<List<CustomerPhoneResponse>>(entityList);
            return new ApiResponse<List<CustomerPhoneResponse>>(mappedList);
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetAllCustomerPhoneByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CustomerPhoneRepository.GetById(request.CustomerId);
            var mapped = mapper.Map<CustomerPhoneResponse>(entity);
            return new ApiResponse<CustomerPhoneResponse>(mapped);
        }

        public async Task<ApiResponse<CustomerPhoneResponse>> Handle(GetCustomerPhoneByParametersQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.CustomerPhoneRepository.GetAll();
            entity.Where(x => x.CustomerId == request.CustomerId).FirstOrDefault();
            var mapped = mapper.Map<CustomerPhoneResponse>(entity);
            return new ApiResponse<CustomerPhoneResponse>(mapped);
        }
    }
}
