using AutoFixture.Xunit2;
using ContactManager.Controllers;
using ContactManager.Models;
using ContactManager.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContactManager.Tests.Controllers
{
    public class ContactControllerTests
    {
        private readonly ContactController contactController;
        private readonly Mock<IContactRepository> contactRepository;

        public ContactControllerTests()
        {
            contactRepository = new Mock<IContactRepository>();
            contactController = new ContactController(contactRepository.Object);
        }

        [Theory]
        [AutoData]
        public void Get_ShouldReturnContacts_IfContactsExists(Contact contact)
        {
            // Arrange
            contactRepository.Setup(cr => cr.GetAllContacts()).Returns(new Contact[] { contact });

            // Act
            var actual = contactController.Get().Length;

            // Assert
            actual.Should().Be(1);
        }
    }
}
