using KT_31_23_Grigorev_Serafim.Models;
using Xunit;




namespace KT_31_23_Grigorev_Serafim.Tests
{

    public class GroupUnitTests
    {

        [Fact]
        public void IsValidGroupName_KT3123_ReturnsTrue()
        {
            // Arrange (Подготовка данных)
            var group = new Group
            {
                Name = "КТ-31-23"
            };

            // Act (Выполнение)
            var result = group.IsValidGroupName();

            // Assert (Проверка результата)
            Assert.True(result);
        }


        [Fact]
        public void IsValidGroupName_InvalidFormat123_ReturnsFalse()
        {
            // Arrange
            var group = new Group
            {
                Name = "123-INVALID"
            };

            // Act
            var result = group.IsValidGroupName();

            // Assert
            Assert.False(result);
        }


        [Fact]
        public void IsValidGroupName_EmptyString_ReturnsFalse()
        {
            // Arrange
            var group = new Group
            {
                Name = ""
            };

            // Act
            var result = group.IsValidGroupName();

            // Assert
            Assert.False(result);
        }

    }

}
