using FluentValidation;
using TMS.Application.TrainingPlans.DTOs;

namespace TMS.Application.TrainingPlans.Validators
{
    public class UpdateTrainingPlanRequestValidator : AbstractValidator<UpdateTrainingPlanRequest>
    {
        public UpdateTrainingPlanRequestValidator()
        {
            RuleFor(x => x.TrainingCategoryId)
                .NotEmpty().WithMessage("TrainingCategoryId is required.");

            RuleFor(x => x.PlanCode)
                .NotEmpty().WithMessage("PlanCode is required.")
                .MaximumLength(50).WithMessage("PlanCode must not exceed 50 characters.");

            RuleFor(x => x.PlanName)
                .NotEmpty().WithMessage("PlanName is required.")
                .MaximumLength(100).WithMessage("PlanName must not exceed 100 characters.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("StartDate must be earlier than EndDate.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("EndDate must be later than StartDate.");

            RuleFor(x => x.TrainingCompany)
                .NotEmpty().WithMessage("TrainingCompany is required.")
                .MaximumLength(100).WithMessage("TrainingCompany must not exceed 100 characters.");

            RuleFor(x => x.TrainingPlace)
                .NotEmpty().WithMessage("TrainingPlace is required.")
                .MaximumLength(200).WithMessage("TrainingPlace must not exceed 200 characters.");

            RuleFor(x => x.TrainingCost)
                .GreaterThanOrEqualTo(0).WithMessage("TrainingCost must be a non-negative value.");

            RuleFor(x => x.ContactPerson)
                .NotEmpty().WithMessage("ContactPerson is required.")
                .MaximumLength(100).WithMessage("ContactPerson must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            
            
        }
        
    }
}