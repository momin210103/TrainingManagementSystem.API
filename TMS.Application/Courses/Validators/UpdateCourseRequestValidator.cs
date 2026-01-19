using FluentValidation;
using TMS.Application.Courses.DTOs;

namespace TMS.Application.Courses.Validators
{
    public class UpdateCourseRequestValidator : AbstractValidator<UpdateCourseRequest>
    {
        public UpdateCourseRequestValidator()
        {
            RuleFor(x => x.CourseCategoryId)
                .NotEmpty();
            RuleFor(x => x.CourseCode)
                .NotEmpty()
                .MaximumLength(20);
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(150);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.DurationHours)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000);
        }
    }
}
