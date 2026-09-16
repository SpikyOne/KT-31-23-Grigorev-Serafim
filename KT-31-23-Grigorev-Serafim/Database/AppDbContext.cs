using KT_31_23_Grigorev_Serafim.Database.Configurations;
using KT_31_23_Grigorev_Serafim.Models;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        // Объявляем таблицы (DbSet) для каждой из 5 моделей
        public DbSet<Specialty> Specialties { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Discipline> Disciplines { get; set; } = null!;
        public DbSet<Grade> Grades { get; set; } = null!;


        // Регистрируем конфигурации Fluent API
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
