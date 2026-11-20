using Microsoft.EntityFrameworkCore;
using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.Models;
using Xunit;

// Отключаем параллельное выполнение, чтобы тесты работали с общей БД последовательно
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace KT_31_23_Grigorev_Serafim.Tests
{
    /// <summary>
    /// Базовый класс для интеграционных тестов с единой In-Memory базой данных.
    /// </summary>
    public abstract class IntegrationTestBase
    {
        protected readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private static bool _isInitialized = false;
        private static readonly object _lock = new();

        protected IntegrationTestBase()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "SharedIntegrationTestDb")
                .Options;

            // Создаем и наполняем общую базу данных ровно один раз для всех тестов
            lock (_lock)
            {
                if (!_isInitialized)
                {
                    using var context = new AppDbContext(_dbContextOptions);
                    context.Database.EnsureCreated();
                    SeedDatabase(context);
                    _isInitialized = true;
                }
            }
        }

        protected AppDbContext CreateContext() => new AppDbContext(_dbContextOptions);

        /// <summary>
        /// Централизованное наполнение единой базы данных вашими тестовыми данными.
        /// </summary>
        private void SeedDatabase(AppDbContext context)
        {
            var specialty1 = new Specialty { Title = "Информатика", Code = "09.03.01" };
            var group1 = new Group { Name = "КТ-31-23", Course = 3, Specialty = specialty1 };

            context.Groups.Add(group1);

            // Студенты
            var studentIvanovIvan = new Student { FirstName = "Иван", LastName = "Иванов", Group = group1, IsDeleted = false };
            var studentIvanovPetr = new Student { FirstName = "Пётр", LastName = "Иванов", Group = group1, IsDeleted = true };
            var studentPetrovIvan = new Student { FirstName = "Иван", LastName = "Петров", Group = group1, IsDeleted = false };

            context.Students.AddRange(studentIvanovIvan, studentIvanovPetr, studentPetrovIvan);

            // Дисциплины
            var disciplineMat = new Discipline { Name = "Математика", IsDeleted = false };
            var disciplineInf = new Discipline { Name = "Информатика", IsDeleted = false };

            context.Disciplines.AddRange(disciplineMat, disciplineInf);

            // 1. иванов иван математика 5
            // 2. иванов петр информатика 4
            // 3. петров иван информатика 5 (или 3, но подставим нужные для теста)
            // 4. иванов петр математика 5
            var grades = new[]
            {
                new Grade { Student = studentIvanovIvan, Discipline = disciplineMat, Value = 5 },
                new Grade { Student = studentIvanovPetr, Discipline = disciplineInf, Value = 5 },
                new Grade { Student = studentPetrovIvan, Discipline = disciplineInf, Value = 5 },
                new Grade { Student = studentIvanovPetr, Discipline = disciplineMat, Value = 5 }
            };

            context.Grades.AddRange(grades);
            context.SaveChanges();
        }
    }
}
