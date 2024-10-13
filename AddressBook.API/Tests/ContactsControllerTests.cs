using AddressBook.API.Controllers;
using AddressBook.API.Models;
using AddressBook.API.Services;
using Moq;

namespace AddressBook.API.Tests.Controllers
{
    public class ContactsControllerTests
    {
        private readonly Mock<IContactService> _mockContactService;
        private readonly ContactsController _controller;

        public ContactsControllerTests()
        {
            _mockContactService = new Mock<IContactService>();
            _controller = new ContactsController(_mockContactService.Object);
        }

        [Fact]
        public async Task GetContacts_ReturnsListOfContacts()
        {
            // Arrange
            var contacts = new List<Contact>
            {
                new Contact { Name = "John Doe", Address = "123 Sunset Boulevard" },
                new Contact { Name = "Jane Doe", Address = "456 Fifth Avenue" }
            };

            _mockContactService.Setup(service => service.GetContactsAsync())
                .ReturnsAsync(contacts);

            // Act
            var result = await _controller.GetContacts();

            // Assert
            var okResult = Assert.IsType<List<Contact>>(result);
            Assert.Equal(2, okResult.Count);
            Assert.Equal("John Doe", okResult[0].Name);
            Assert.Equal("Jane Doe", okResult[1].Name);
        }

        [Fact]
        public async Task GetContacts_ReturnsEmptyList_WhenNoContacts()
        {
            // Arrange
            var contacts = new List<Contact>();

            _mockContactService.Setup(service => service.GetContactsAsync())
                .ReturnsAsync(contacts);

            // Act
            var result = await _controller.GetContacts();

            // Assert
            var okResult = Assert.IsType<List<Contact>>(result);
            Assert.Empty(okResult);
        }
    }
}
