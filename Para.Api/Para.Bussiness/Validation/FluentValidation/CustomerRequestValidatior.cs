using FluentValidation;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation.FluentValidation
{
    public class CustomerRequestValidatior : AbstractValidator<CustomerRequest>
    {
        public CustomerRequestValidatior()
        {
            RuleFor(x => x.CustomerNumber).GreaterThan(0).NotEmpty().WithMessage($"{nameof(CustomerRequest.CustomerNumber)} alani boş geçilemez");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage($"{nameof(CustomerRequest.Email)} alani boş geçilemez");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage($"{nameof(CustomerRequest.FirstName)} alani boş geçilemez");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage($"{nameof(CustomerRequest.LastName)} alani boş geçilemez");
            RuleFor(x => x.IdentityNumber).NotNull().NotEmpty().WithMessage($"{nameof(CustomerRequest.IdentityNumber)} alani boş geçilemez");
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().WithMessage($"{nameof(CustomerRequest.DateOfBirth)} alani boş geçilemez");


        }
    }
}
