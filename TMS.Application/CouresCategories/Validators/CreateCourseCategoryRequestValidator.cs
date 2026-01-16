using FluentValidation;
using TMS.Application.CouresCategories.DTOs;

namespace TMS.Application.CouresCategories.Validators
{
    public class CreateCourseCategoryRequestValidator : AbstractValidator<CreateCourseCategoryRequest>
    {
        public CreateCourseCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}
