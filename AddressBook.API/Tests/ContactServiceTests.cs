using Microsoft.Extensions.Logging;
using Moq;
using System.IO.Abstractions;

namespace AddressBook.API.Services.Tests
{
    public class ContactServiceTests
    {
        private readonly Mock<ILogger<ContactService>> _mockLogger;
        private readonly Mock<IFileSystem> _mockFileSystem;
        private readonly Mock<IFile> _mockFile;
        private readonly ContactService _contactService;

        public ContactServiceTests()
        {
            _mockLogger = new Mock<ILogger<ContactService>>();
            _mockFileSystem = new Mock<IFileSystem>();
            _mockFile = new Mock<IFile>();
            
            _mockFileSystem.Setup(fs => fs.File).Returns(_mockFile.Object);
            _contactService = new ContactService(_mockLogger.Object, _mockFileSystem.Object);
        }

        [Fact]
        public async Task GetContactsAsync_ReturnsEmptyList_WhenFileDoesNotExist()
        {
            // Arrange
            _mockFileSystem.Setup(fs => fs.File.Exists(It.IsAny<string>())).Returns(false);

            // Act
            var result = await _contactService.GetContactsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetContactsAsync_ReturnsEmptyList_WhenFileIsEmpty()
        {
            // Arrange
            _mockFile.Setup(f => f.Exists(It.IsAny<string>())).Returns(true);
            _mockFileSystem.Setup(fs => fs.File.ReadAllTextAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(string.Empty);

            // Act
            var result = await _contactService.GetContactsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetContactsAsync_ReturnsContacts_WhenFileContainsValidJson()
        {
            // Arrange
            var json = "[{\"Name\": \"John Doe\", \"Address\": \"123 Sunset Boulevard\"}]";

            
            _mockFileSystem.Setup(fs => fs.File.Exists(It.IsAny<string>())).Returns(true);            
            _mockFileSystem.Setup(fs => fs.File.ReadAllTextAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(json);

            // Act
            var result = await _contactService.GetContactsAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("John Doe", result.First().Name);
        }

        [Fact]
        public async Task GetContactsAsync_ReturnsEmptyList_WhenJsonIsInvalid()
        {
            // Arrange
            var invalidJson = "{invalid json}";
            _mockFileSystem.Setup(fs => fs.File.Exists(It.IsAny<string>())).Returns(true);
            _mockFileSystem.Setup(fs => fs.File.ReadAllTextAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(invalidJson);

            // Act
            var result = await _contactService.GetContactsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetContactsAsync_ReturnsEmptyList_WhenExceptionOccurs()
        {
            // Arrange
            _mockFileSystem.Setup(fs => fs.File.Exists(It.IsAny<string>())).Throws(new IOException("File access error"));

            // Act
            var result = await _contactService.GetContactsAsync();

            // Assert
            Assert.Empty(result);
        }
    }
}