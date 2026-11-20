using KT_31_23_Grigorev_Serafim.Models;
using Xunit;

namespace KT_31_23_Grigorev_Serafim.Tests
{
    /// <summary>
    /// Юнит-тесты для проверки доменной логики модели <see cref="Discipline"/>.
    /// </summary>
    public class DisciplineUnitTests
    {
        /// <summary>
        /// Проверяет, что корректное наименование дисциплины (например, "Базы данных") 
        /// успешно проходит валидацию и возвращает <c>true</c>.
        /// </summary>
        [Fact]
        public void IsValidDisciplineName_ValidName_ReturnsTrue()
        {
            // Arrange
            var disciplineName = "Базы данных";

            // Act
            var result = Discipline.IsValidDisciplineName(disciplineName);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// Проверяет, что наименование дисциплины с недопустимыми символами (например, цифрами или латиницей) 
        /// не проходит валидацию и возвращает <c>false</c>.
        /// </summary>
        [Fact]
        public void IsValidDisciplineName_InvalidCharacters_ReturnsFalse()
        {
            // Arrange
            var disciplineName = "Математика-2";

            // Act
            var result = Discipline.IsValidDisciplineName(disciplineName);

            // Assert
            Assert.False(result);
        }

        /// <summary>
        /// Проверяет, что передача пустой строки или пробелов 
        /// признаётся невалидной и возвращает <c>false</c>.
        /// </summary>
        [Fact]
        public void IsValidDisciplineName_EmptyOrWhitespaceString_ReturnsFalse()
        {
            // Arrange
            var disciplineName = "   ";

            // Act
            var result = Discipline.IsValidDisciplineName(disciplineName);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidDisciplineName_regultest()
        {
            // Arrange
            var disciplineName = "приветприве";

            // Act
            var result = Discipline.IsValidDisciplineName(disciplineName);

            // Assert
            Assert.True(result);
        }

    }
}
