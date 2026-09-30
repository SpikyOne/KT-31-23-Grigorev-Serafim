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
    public class StudentDisciplinesServiceIntegrationTests
    {

        private readonly DbContextOptions<AppDbContext> _dbContextOptions;


        public StudentDisciplinesServiceIntegrationTests()
        {

            // Уникальная база данных в памяти для каждого тестового прогона
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

        }


        /// <summary>
        /// Проверяет выборку дисциплин по фамилии студента: попадают только дисциплины с оценкой 5, 
        /// дубликаты исключаются (Distinct), а итоговый список отсортирован по алфавиту.
        /// </summary>
        [Fact]
        public async Task GetExcellentDisciplinesByLastNameAsync_ValidLastName_ReturnsSortedUniqueDisciplinesWithGrade5()
        {

            // Arrange (Подготовка данных)
            using var context = new AppDbContext(_dbContextOptions);

            var specialty = new Specialty { Title = "Информатика", Code = "09.03.01" };
            var group = new Group { Name = "КТ-31-23", Course = 3, Specialty = specialty };

            var student = new Student
            {
                FirstName = "Серафим",
                LastName = "Григорьев",
                Group = group,
                IsDeleted = false
            };

            var discipline1 = new Discipline { Name = "Математика", IsDeleted = false };
            var discipline2 = new Discipline { Name = "Физика", IsDeleted = false };
            var discipline3 = new Discipline { Name = "Базы данных", IsDeleted = false };

            var grades = new[]
            {
                new Grade { Student = student, Discipline = discipline1, Value = 5 },
                new Grade { Student = student, Discipline = discipline1, Value = 5 }, // Повторная оценка 5 (проверка Distinct)
                new Grade { Student = student, Discipline = discipline2, Value = 4 }, // Оценка не 5 (не должна попасть)
                new Grade { Student = student, Discipline = discipline3, Value = 5 }  // Оценка 5
            };

            await context.Grades.AddRangeAsync(grades);
            await context.SaveChangesAsync();

            var service = new StudentDisciplinesService(context);
            var filter = new StudentLastNameFilter { LastName = "Григорьев" };

            // Act (Выполнение)
            var result = await service.GetExcellentDisciplinesByLastNameAsync(filter, CancellationToken.None);

            // Assert (Проверка результатов)
            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            Assert.Equal("Базы данных", result[0]); // Алфавитная сортировка: "Базы данных" идет раньше "Математика"
            Assert.Equal("Математика", result[1]);

        }


        /// <summary>
        /// Проверяет, что передача значения <c>null</c>, пустой строки или строки из пробелов 
        /// в фильтре по фамилии возвращает пустой массив без ошибок.
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetExcellentDisciplinesByLastNameAsync_NullOrEmptyOrWhitespaceLastName_ReturnsEmptyArray(string? lastName)
        {
            // Arrange
            using var context = new AppDbContext(_dbContextOptions);
            var service = new StudentDisciplinesService(context);
            var filter = new StudentLastNameFilter { LastName = lastName! };

            // Act
            var result = await service.GetExcellentDisciplinesByLastNameAsync(filter, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

        }


        /// <summary>
        /// Проверяет регистронезависимость поиска по фамилии и удаление внешних пробелов (Trim) в параметрах фильтра.
        /// </summary>
        [Fact]
        public async Task GetExcellentDisciplinesByLastNameAsync_CaseInsensitiveAndTrimming_ReturnsDisciplines()
        {

            // Arrange
            using var context = new AppDbContext(_dbContextOptions);

            var specialty = new Specialty { Title = "ПО", Code = "09.03.04" };
            var group = new Group { Name = "КТ-31-23", Course = 3, Specialty = specialty };

            var student = new Student
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Group = group,
                IsDeleted = false
            };

            var discipline = new Discipline { Name = "Программирование", IsDeleted = false };
            var grade = new Grade { Student = student, Discipline = discipline, Value = 5 };

            await context.Grades.AddAsync(grade);
            await context.SaveChangesAsync();

            var service = new StudentDisciplinesService(context);
            // Передаем фамилию с разным регистром и пробелами
            var filter = new StudentLastNameFilter { LastName = "  иВаНоВ  " };

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
            using var context = new AppDbContext(_dbContextOptions);

            var specialty = new Specialty { Title = "ПО", Code = "09.03.04" };
            var group = new Group { Name = "КТ-31-23", Course = 3, Specialty = specialty };

            var activeStudent = new Student { FirstName = "Иван", LastName = "Петров", Group = group, IsDeleted = false };
            var deletedStudent = new Student { FirstName = "Пётр", LastName = "Петров", Group = group, IsDeleted = true };

            var activeDiscipline = new Discipline { Name = "Алгоритмы", IsDeleted = false };
            var deletedDiscipline = new Discipline { Name = "История", IsDeleted = true };

            var grades = new[]
            {
                new Grade { Student = activeStudent, Discipline = activeDiscipline, Value = 5 },   // Активный студент и активный предмет -> попадает
                new Grade { Student = activeStudent, Discipline = deletedDiscipline, Value = 5 },  // Предмет удален -> игнорируется
                new Grade { Student = deletedStudent, Discipline = activeDiscipline, Value = 5 }   // Студент удален -> игнорируется
            };

            await context.Grades.AddRangeAsync(grades);
            await context.SaveChangesAsync();

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
            using var context = new AppDbContext(_dbContextOptions);
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
