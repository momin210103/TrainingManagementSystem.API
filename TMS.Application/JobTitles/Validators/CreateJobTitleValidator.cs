using FluentValidation;
using TMS.Application.JobTitles.DTOs;

namespace TMS.Application.JobTitles.Validators
{
    public class CreateJobTitleValidator : AbstractValidator<CreateJobTitleRequest>
    {
        public CreateJobTitleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
