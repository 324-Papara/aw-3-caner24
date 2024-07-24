using FluentValidation;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation.FluentValidation
{
    public class CustomerAdressRequestValidator : AbstractValidator<CustomerAddressRequest>
    {
        public CustomerAdressRequestValidator()
        {
            RuleFor(x => x.IsDefault).NotEmpty().WithMessage($"{nameof(CustomerAddressRequest.IsDefault)} alani boş geçilemez");
            RuleFor(x => x.AddressLine).NotNull().NotEmpty().WithMessage($"{nameof(CustomerAddressRequest.AddressLine)} alani boş geçilemez");
            RuleFor(x => x.ZipCode).NotNull().NotEmpty().WithMessage($"{nameof(CustomerAddressRequest.ZipCode)} alani boş geçilemez");
            RuleFor(x => x.CustomerId).NotNull().NotEmpty().WithMessage($"{nameof(CustomerAddressRequest.CustomerId)} alani boş geçilemez");
            RuleFor(x => x.Country).NotNull().NotEmpty().WithMessage($"{nameof(CustomerAddressRequest.Country)} alani boş geçilemez");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage($"{nameof(CustomerAddressRequest.City)} alani boş geçilemez");


        }
    }
}
