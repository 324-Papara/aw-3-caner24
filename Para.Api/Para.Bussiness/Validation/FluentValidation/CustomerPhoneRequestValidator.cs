using FluentValidation;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation.FluentValidation
{
    public class CustomerPhoneRequestValidator:AbstractValidator<CustomerPhoneRequest>
    {
        public CustomerPhoneRequestValidator()
        {
            RuleFor(x => x.IsDefault).NotEmpty().WithMessage($"{nameof(CustomerPhoneRequest.IsDefault)} alani boş geçilemez");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage($"{nameof(CustomerPhoneRequest.Phone)} alani boş geçilemez");
            RuleFor(x => x.CountyCode).NotNull().NotEmpty().WithMessage($"{nameof(CustomerPhoneRequest.CountyCode)} alani boş geçilemez");


        }
    }
}
