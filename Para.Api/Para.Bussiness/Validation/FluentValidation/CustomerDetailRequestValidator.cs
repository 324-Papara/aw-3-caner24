using FluentValidation;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation.FluentValidation
{
    public class CustomerDetailRequestValidator : AbstractValidator<CustomerDetailRequest>
    {
        public CustomerDetailRequestValidator()
        {
            RuleFor(x => x.FatherName).NotEmpty().WithMessage($"{nameof(CustomerDetailRequest.FatherName)} alani boş geçilemez");
            RuleFor(x => x.EducationStatus).NotNull().NotEmpty().WithMessage($"{nameof(CustomerDetailRequest.EducationStatus)} alani boş geçilemez");
            RuleFor(x => x.Occupation).NotNull().NotEmpty().WithMessage($"{nameof(CustomerDetailRequest.Occupation)} alani boş geçilemez");
            RuleFor(x => x.MontlyIncome).NotNull().NotEmpty().WithMessage($"{nameof(CustomerDetailRequest.MontlyIncome)} alani boş geçilemez");
            RuleFor(x => x.MotherName).NotNull().NotEmpty().WithMessage($"{nameof(CustomerDetailRequest.MotherName)} alani boş geçilemez");


        }
    }
}
