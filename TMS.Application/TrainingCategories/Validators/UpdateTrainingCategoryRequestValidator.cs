using FluentValidation;
using TMS.Application.TrainingCategories.DTOs;

namespace TMS.Application.TrainingCategories.Validators
{
    public class UpdateTrainingCategoryRequestValidator : AbstractValidator<UpdateTrainingCategoryRequest>
    {
        public UpdateTrainingCategoryRequestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            
        }
    }
}