using KT_31_23_Grigorev_Serafim.Models;
using Xunit;




namespace KT_31_23_Grigorev_Serafim.Tests
{

    /// <summary>
    /// Юнит-тесты для проверки доменной логики модели <see cref="Group"/>.
    /// </summary>
    public class GroupUnitTests
    {

        /// <summary>
        /// Проверяет, что корректное наименование группы (например, "КТ-31-23") 
        /// успешно проходит валидацию и возвращает <c>true</c>.
        /// </summary>
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


        /// <summary>
        /// Проверяет, что наименование группы неверного формата (например, "123-INVALID") 
        /// не проходит валидацию и возвращает <c>false</c>.
        /// </summary>
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


        /// <summary>
        /// Проверяет, что передача пустой строки в качестве наименования группы 
        /// признаётся невалидной и возвращает <c>false</c>.
        /// </summary>
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
