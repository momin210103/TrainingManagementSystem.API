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
    [Fact]
    public async Task CreateAsync_WhenValidationFails_ShouldThrowValidationException()
    {
        // Arrange

        TmsDbContext context =
            DbContextHelper.CreateInMemoryDbContext();

        var createValidatorMock =
            new Mock<IValidator<CreateCourseRequest>>();

        var updateValidatorMock =
            new Mock<IValidator<UpdateCourseRequest>>();

        var loggerMock =
            new Mock<ILogger<CourseService>>();

        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Title", "Title is required")
        };

        createValidatorMock
            .Setup(v => v.ValidateAsync(
                It.IsAny<CreateCourseRequest>(),
                default))
            .ReturnsAsync(new ValidationResult(failures));

        var service = new CourseService(
            context,
            createValidatorMock.Object,
            updateValidatorMock.Object,
            loggerMock.Object);

        var request = new CreateCourseRequest();

        // Act

        Func<Task> action = async () =>
            await service.CreateAsync(request);

        // Assert

        await action.Should()
            .ThrowAsync<ValidationException>();
    }
    
}