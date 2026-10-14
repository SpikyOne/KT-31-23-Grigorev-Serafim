using Microsoft.EntityFrameworkCore;
using KT_31_23_Grigorev_Serafim.Database;
using Xunit;




// Полностью отключаем параллельное выполнение тестов в сборке,
// чтобы тесты с общей базой данных не конфликтовали друг с другом.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace KT_31_23_Grigorev_Serafim.Tests
{

    /// <summary>
    /// Базовый класс для интеграционных тестов с общей In-Memory базой данных.
    /// </summary>
    public abstract class IntegrationTestBase
    {

        protected readonly DbContextOptions<AppDbContext> _dbContextOptions;

        protected IntegrationTestBase()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "SharedIntegrationTestDb")
                .Options;

            // Очищаем и создаем заново базу перед каждым тестом для полной изоляции данных
            using var context = new AppDbContext(_dbContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        protected AppDbContext CreateContext() => new AppDbContext(_dbContextOptions);

    }

}
