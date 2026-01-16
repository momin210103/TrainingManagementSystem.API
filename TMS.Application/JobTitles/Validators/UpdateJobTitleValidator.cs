using FluentValidation;
using TMS.Application.JobTitles.DTOs;

namespace TMS.Application.JobTitles.Validators
{
    public class UpdateJobTitleValidator: AbstractValidator<UpdateJobTitleRequest>
    {
        public UpdateJobTitleValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
