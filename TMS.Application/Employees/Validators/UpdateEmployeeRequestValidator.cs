using FluentValidation;
using TMS.Application.Employees.DTOs;

namespace TMS.Application.Employees.Validators
{
    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.EmployeeCode)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
