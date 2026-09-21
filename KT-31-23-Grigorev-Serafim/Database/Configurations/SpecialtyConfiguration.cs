using KT_31_23_Grigorev_Serafim.Database.Helpers;
using KT_31_23_Grigorev_Serafim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;




namespace KT_31_23_Grigorev_Serafim.Database.Configurations
{

    /// <summary>
    /// Конфигурация Fluent API для сущности <see cref="Specialty"/>
    /// </summary>
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {

        private const string TableName = "cd_specialty";

        /// <summary>
        /// Конфигурирует маппинг специальности на таблицу БД
        /// </summary>
        /// <param name="builder">Строитель конфигурации сущности</param>
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(p => p.SpecialtyId)
                   .HasName($"pk_{TableName}_specialty_id");

            builder.Property(p => p.SpecialtyId)
                   .ValueGeneratedOnAdd()
                   .HasColumnName("c_specialty_id")
                   .HasComment("Идентификатор специальности");

            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasColumnName("c_specialty_title")
                   .HasColumnType(ColumnType.String).HasMaxLength(100)
                   .HasComment("Название специальности");

            builder.Property(p => p.Code)
                   .IsRequired()
                   .HasColumnName("c_specialty_code")
                   .HasColumnType(ColumnType.String).HasMaxLength(20)
                   .HasComment("Код специальности");
        }

    }

}
