using FluentValidation;
using TMS.Application.CouresCategories.DTOs;

namespace TMS.Application.CouresCategories.Validators
{
    public class UpdateCourseCategoryRequestValidator : AbstractValidator<UpdateCourseCategoryRequest>
    {
        public UpdateCourseCategoryRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
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
