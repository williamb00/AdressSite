using Xunit;

namespace ConsoleApp.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void AddContact_ShouldAddContactToList()
        {
            // Arrange
            Program.LoadContacts();
            Program programInstance = new Program();

            // Clear existing contacts for a clean slate
            Program.contacts.Clear();

            // Act
            programInstance.AddContact("John Doe", "123456789", "john@example.com", "123 Main St", "City", "123456789");

            // Assert
            Assert.Single(Program.contacts);
        }
    }
}



