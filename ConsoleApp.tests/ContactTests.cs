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
            programInstance.AddContact("William Björklund", "telefon", "william@gmail.com", "Bokvägen 5", "Stad", "Person nummmer");

            // Assert
            Assert.Single(Program.contacts);
        }
    }
}



