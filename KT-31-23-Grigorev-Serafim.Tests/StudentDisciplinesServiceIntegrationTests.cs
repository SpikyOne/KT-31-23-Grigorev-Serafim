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

        [Fact]
        public async Task GetExcellentDisciplinesByLastNameAsync_NullOrEmptyOrWhitespaceLastName_ReturnsEmptyArray()
        {
            var invalidValues = new string?[] { null, "", "   " };

            foreach (var lastName in invalidValues)
            {
                using var context = CreateContext();
                var service = new StudentDisciplinesService(context);
                var filter = new StudentLastNameFilter { LastName = lastName! };

                var result = await service.GetExcellentDisciplinesByLastNameAsync(filter, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Empty(result);
            }
        }


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


        [Fact]
        public async Task GetExcellentDisciplinesByLastNameAsync_sdd1()
        {
            // Arrange
            using var context = CreateContext();
            var service = new StudentDisciplinesService(context);
            var filter = new StudentLastNameFilter { LastName = "Иванов" };

            // Act
            var result = await service.GetExcellentDisciplinesByLastNameAsync(filter, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Length);
        }
    }
}
