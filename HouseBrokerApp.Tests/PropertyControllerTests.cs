using Xunit;
using Moq;
using FluentAssertions;
using HouseBrokerApp.Api.Controllers;
using HouseBrokerApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using HouseBrokerApp.Infrastructure.Interfaces;

namespace HouseBrokerApp.Tests
{
    public class PropertyControllerTests
    {
        private readonly Mock<IPropertyRepository> _mockRepo;
        private readonly PropertyController _controller;

        public PropertyControllerTests()
        {
            _mockRepo = new Mock<IPropertyRepository>();
            _controller = new PropertyController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithProperties()
        {
            // Arrange
            var properties = new List<Property>
            {
                new Property { Id = Guid.NewGuid(), Title = "Loft", Location = "NYC" },
                new Property { Id = Guid.NewGuid(), Title = "Villa", Location = "LA" }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(properties);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedProperties = okResult.Value.Should().BeAssignableTo<IEnumerable<Property>>().Subject;
            returnedProperties.Should().HaveCount(2);
        }

        [Fact]
        public async Task Get_ShouldReturnNotFound_WhenPropertyDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Property?)null);

            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Search_ShouldReturnOk_WithFilteredResults()
        {
            // Arrange
            var properties = new List<Property>
            {
                new Property { Id = Guid.NewGuid(), Location = "NYC" }
            };
            _mockRepo.Setup(repo => repo.SearchAsync("NYC", null, null, null)).ReturnsAsync(properties);

            // Act
            var result = await _controller.Search("NYC", null, null, null);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedProperties = okResult.Value.Should().BeAssignableTo<IEnumerable<Property>>().Subject;
            returnedProperties.Should().HaveCount(1);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WhenValid()
        {
            // Arrange
            var newProperty = new Property { Title = "Loft" };
            _mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Property>())).ReturnsAsync(newProperty);

            // Act
            var result = await _controller.Create(newProperty);

            // Assert
            var createdResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdResult.ActionName.Should().Be(nameof(_controller.Get));
            createdResult.Value.Should().BeOfType<Property>();
        }
    }
}
