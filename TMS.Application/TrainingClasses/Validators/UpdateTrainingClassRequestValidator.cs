using FluentValidation;
using TMS.Application.TrainingClasses.DTOs;

namespace TMS.Application.TrainingClasses.Validators
{
    public class UpdateTrainingClassRequestValidator : AbstractValidator<UpdateTrainingClassRequest>
    {
        public UpdateTrainingClassRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("StartDate must be earlier than EndDate.");
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate);
            RuleFor(x => x.Capacity)
                .GreaterThan(0);
            RuleFor(x => x.Status)
                .NotEmpty();
        }
    }
}