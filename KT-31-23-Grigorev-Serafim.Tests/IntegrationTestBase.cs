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
        /// Централизованное наполнение единой базы данных всеми необходимыми тестовыми данными.
        /// </summary>
        private void SeedDatabase(AppDbContext context)
        {
            var specialty1 = new Specialty { Title = "Информатика", Code = "09.03.01" };
            var specialty2 = new Specialty { Title = "ПО", Code = "09.03.04" };

            var group1 = new Group { Name = "КТ-31-23", Course = 3, Specialty = specialty1 };
            var group2 = new Group { Name = "КТ-41-23", Course = 4, Specialty = specialty2 };

            context.Groups.AddRange(group1, group2);

            // Студенты для StudentDisciplinesService
            var studentGrigoriev = new Student { FirstName = "Серафим", LastName = "Григорьев", Group = group1, IsDeleted = false };
            var studentIvanov = new Student { FirstName = "Иван", LastName = "Иванов", Group = group2, IsDeleted = false };
            var activeStudentPetrov = new Student { FirstName = "Иван", LastName = "Петров", Group = group1, IsDeleted = false };
            var deletedStudent = new Student { FirstName = "Пётр", LastName = "Петров", Group = group1, IsDeleted = true };

            // Студенты для StudentService (фильтрация по группе)
            var studentExtra1 = new Student { FirstName = "Серафим", LastName = "Григорьев", Group = group1, IsDeleted = false };
            var studentExtra2 = new Student { FirstName = "Иван", LastName = "Иванов", Group = group1, IsDeleted = false };
            var studentExtra3 = new Student { FirstName = "Пётр", LastName = "Петров", Group = group2, IsDeleted = false };

            context.Students.AddRange(studentGrigoriev, studentIvanov, activeStudentPetrov, deletedStudent, studentExtra1, studentExtra2, studentExtra3);

            var discipline1 = new Discipline { Name = "Математика", IsDeleted = false };
            var discipline2 = new Discipline { Name = "Физика", IsDeleted = false };
            var discipline3 = new Discipline { Name = "Базы данных", IsDeleted = false };
            var discipline4 = new Discipline { Name = "Программирование", IsDeleted = false };
            var discipline5 = new Discipline { Name = "Алгоритмы", IsDeleted = false };
            var discipline6 = new Discipline { Name = "История", IsDeleted = true };

            var grades = new[]
            {
                // Григорьев (оценки 5, 5 (дубль), 4, 5) -> должны остаться "Базы данных" и "Математика"
                new Grade { Student = studentGrigoriev, Discipline = discipline1, Value = 5 },
                new Grade { Student = studentGrigoriev, Discipline = discipline1, Value = 5 },
                new Grade { Student = studentGrigoriev, Discipline = discipline2, Value = 4 },
                new Grade { Student = studentGrigoriev, Discipline = discipline3, Value = 5 },

                // Иванов (для тестов регистронезависимости с пробелами)
                new Grade { Student = studentIvanov, Discipline = discipline4, Value = 5 },

                // Петров (для Soft Delete)
                new Grade { Student = activeStudentPetrov, Discipline = discipline5, Value = 5 },
                new Grade { Student = activeStudentPetrov, Discipline = discipline6, Value = 5 }, // Удаленный предмет
                new Grade { Student = deletedStudent, Discipline = discipline5, Value = 5 }       // Удаленный студент
            };

            context.Grades.AddRange(grades);
            context.SaveChanges();
        }
    }
}
