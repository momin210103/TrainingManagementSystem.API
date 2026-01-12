using FluentValidation;
using TMS.Application.Employees.DTOs;

namespace TMS.Application.Employees.Validators
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.EmployeeCode)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.DepartmentId)
                .NotEqual(Guid.Empty)
                .When(x => x.DepartmentId.HasValue);
            RuleFor(x => x.JobTitleId)
                .NotEqual(Guid.Empty)
                .When(x => x.DepartmentId.HasValue);
        }
    }
}
