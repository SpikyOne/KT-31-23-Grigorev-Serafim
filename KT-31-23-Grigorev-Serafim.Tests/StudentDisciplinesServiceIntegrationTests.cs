using System;
using System.Threading;
using System.Threading.Tasks;
using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Models;
using KT_31_23_Grigorev_Serafim.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;




namespace KT_31_23_Grigorev_Serafim.Tests
{

    /// <summary>
    /// Интеграционные тесты для проверки сервиса получения отличных дисциплин <see cref="StudentDisciplinesService"/>.
    /// </summary>
    public class StudentDisciplinesServiceIntegrationTests : IntegrationTestBase
    {

        /// <summary>
        /// Проверяет выборку дисциплин по фамилии студента: попадают только дисциплины с оценкой 5, 
        /// дубликаты исключаются (Distinct), а итоговый список отсортирован по алфавиту.
        /// </summary>
        [Fact]
        public async Task GetExcellentDisciplinesByLastNameAsync_ValidLastName_ReturnsSortedUniqueDisciplinesWithGrade5()
        {

            // Arrange
            using var context = CreateContext();

            var service = new StudentDisciplinesService(context);
            var filter = new StudentLastNameFilter { LastName = "Григорьев" };

            // Act
            var result = await service.GetExcellentDisciplinesByLastNameAsync(filter, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            Assert.Equal("Базы данных", result[0]);         // Алфавитная сортировка: "Базы данных" идет раньше "Математика"
            Assert.Equal("Математика", result[1]);

        }


        /// <summary>
        /// Проверяет, что передача значения <c>null</c>, пустой строки или строки из пробелов 
        /// в фильтре по фамилии возвращает пустой массив без ошибок.
        /// </summary>
        [Fact]
        public async Task GetExcellentDisciplinesByLastNameAsync_NullOrEmptyOrWhitespaceLastName_ReturnsEmptyArray()
        {

            // Arrange
            var invalidValues = new string?[] { null, "", "   " };

            foreach (var lastName in invalidValues)
            {
                using var context = CreateContext();
                var service = new StudentDisciplinesService(context);
                var filter = new StudentLastNameFilter { LastName = lastName! };

                // Act
                var result = await service.GetExcellentDisciplinesByLastNameAsync(filter, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Empty(result);

            }

        }


        /// <summary>
        /// Проверяет регистронезависимость поиска по фамилии и удаление внешних пробелов (Trim) в параметрах фильтра.
        /// </summary>
        [Fact]
        public async Task GetExcellentDisciplinesByLastNameAsync_CaseInsensitiveAndTrimming_ReturnsDisciplines()
        {
            // Arrange
            using var context = CreateContext();

            var service = new StudentDisciplinesService(context);
            var filter = new StudentLastNameFilter { LastName = "   иВаНоВ   " };

            // Act
            var result = await service.GetExcellentDisciplinesByLastNameAsync(filter, CancellationToken.None);

            // Assert
            Assert.Single(result);
            Assert.Equal("Программирование", result[0]);

        }


        /// <summary>
        /// Проверяет корректность мягкого удаления (Soft Delete): игнорируются ли записи 
        /// со свойствами <c>IsDeleted = true</c> для студентов и дисциплин.
        /// </summary>
        [Fact]
        public async Task GetExcellentDisciplinesByLastNameAsync_IgnoresDeletedStudentAndDeletedDiscipline()
        {

            // Arrange
            using var context = CreateContext();

            var service = new StudentDisciplinesService(context);
            var filter = new StudentLastNameFilter { LastName = "Петров" };

            // Act
            var result = await service.GetExcellentDisciplinesByLastNameAsync(filter, CancellationToken.None);

            // Assert
            Assert.Single(result);
            Assert.Equal("Алгоритмы", result[0]);

        }


        /// <summary>
        /// Проверяет, что при поиске дисциплин для несуществующей фамилии возвращается пустой массив.
        /// </summary>
        [Fact]
        public async Task GetExcellentDisciplinesByLastNameAsync_NonExistentLastName_ReturnsEmptyArray()
        {

            // Arrange
            using var context = CreateContext();
            var service = new StudentDisciplinesService(context);
            var filter = new StudentLastNameFilter { LastName = "Несуществующий" };

            // Act
            var result = await service.GetExcellentDisciplinesByLastNameAsync(filter, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

        }

    }

}
