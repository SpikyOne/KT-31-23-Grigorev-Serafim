using KT_31_23_Grigorev_Serafim.Database.Configurations;
using KT_31_23_Grigorev_Serafim.Models;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Database
{

    /// <summary>
    /// Контекст базы данных Entity Framework Core для приложения
    /// </summary>
    public class AppDbContext : DbContext
    {

        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных
        /// </summary>
        /// <param name="options">Параметры конфигурации контекста БД</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        // Объявляем таблицы (DbSet) для каждой из 5 моделей
        
        /// <summary>Таблица специальностей</summary>
        public DbSet<Specialty> Specialties { get; set; } = null!;

        /// <summary>Таблица учебных групп</summary>
        public DbSet<Group> Groups { get; set; } = null!;

        /// <summary>Таблица студентов</summary>
        public DbSet<Student> Students { get; set; } = null!;

        /// <summary>Таблица учебных дисциплин</summary>
        public DbSet<Discipline> Disciplines { get; set; } = null!;

        /// <summary>Таблица оценок успеваемости</summary>
        public DbSet<Grade> Grades { get; set; } = null!;


        /// <summary>
        /// Настройка конфигураций моделей и связей базы данных через Fluent API
        /// </summary>
        /// <param name="modelBuilder">Строитель моделей БД</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Добавляем конфигурации к таблицам
            modelBuilder.ApplyConfiguration(new SpecialtyConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
        }

    }

}
