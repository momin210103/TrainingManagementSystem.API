using FluentValidation;
using TMS.Application.Enrollments.DTOs;

namespace TMS.Application.Enrollments.Validators
{
    public class CreateEnrollmentRequestValidator : AbstractValidator<CreateEnrollmentRequest>
    {
        public CreateEnrollmentRequestValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.EmployeeId)
                .NotEmpty();
            RuleFor(x => x.TrainingClassId)
                .NotEmpty();

             
        }
    }
}
