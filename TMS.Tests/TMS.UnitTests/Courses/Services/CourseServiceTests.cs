using Xunit;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using TMS.Application.Courses.DTOs;
using TMS.Application.Courses.Services;
using TMS.Infrastructure.Persistence;
using TMS.UnitTests.Courses.Helpers;

namespace TMS.UnitTests.Courses.Services;

public class CourseServiceTests
{
    private readonly TmsDbContext _context;
    private readonly Mock<IValidator<CreateCourseRequest>> _createValidatorMock;
    private readonly Mock<IValidator<UpdateCourseRequest>> _updateValidatorMock;
    private readonly Mock<ILogger<CourseService>> _loggerMock;
    private readonly CourseService _service;

    public CourseServiceTests()
    {
        _context = DbContextHelper.CreateInMemoryDbContext();
        _createValidatorMock = new Mock<IValidator<CreateCourseRequest>>();
        _updateValidatorMock = new Mock<IValidator<UpdateCourseRequest>>();
        _loggerMock = new Mock<ILogger<CourseService>>();
        _service = new CourseService(_context, _createValidatorMock.Object, _updateValidatorMock.Object, _loggerMock.Object);

    }
    [Fact]
    public async Task CreateAsync_WhenValidationFails_ShouldThrowValidationException()
    {
        // Arrange
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Title", "Title is required")
        };

        _createValidatorMock
            .Setup(v => v.ValidateAsync(
                It.IsAny<CreateCourseRequest>(),
                default))
            .ReturnsAsync(new ValidationResult(failures));
        

        var request = new CreateCourseRequest();

        // Act

        Func<Task> action = async () =>
            await _service.CreateAsync(request);

        // Assert

        await action.Should()
            .ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateAsync_WhenCategoryDoesntExist_ShouldThrowCategoryNotFoundException()
    {

        // Validation should pass
        _createValidatorMock
            .Setup(v => v.ValidateAsync(
                It.IsAny<CreateCourseRequest>(),
                default))
            .ReturnsAsync(new ValidationResult());

        var request = new CreateCourseRequest
        {
            CourseCategoryId = Guid.NewGuid(),
            CourseCode = "CSE101",
            Title = "ASP.NET Core",
            Description = "Backend Development",
            DurationHours = 40
        };

        // Act

        Func<Task> action = async () =>
            await _service.CreateAsync(request);

        // Assert

        await action.Should()
            .ThrowAsync<KeyNotFoundException>()
            .WithMessage("Course Category not found.");
    }
    
}