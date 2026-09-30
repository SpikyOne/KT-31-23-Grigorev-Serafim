using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.DTOs.Database;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Models;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Services
{

    /// <summary>
    /// Сервис для глобального обслуживания базы данных (каскадная очистка, экспорт дампа и атомарное восстановление).
    /// </summary>
    public class DatabaseService : IDatabaseService
    {

        private readonly AppDbContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр сервиса обслуживания базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных Entity Framework Core.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="context"/> равен <c>null</c>.</exception>
        public DatabaseService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Выполняет полную каскадную очистку всех таблиц базы данных и сбрасывает счетчики автоинкремента.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <exception cref="InvalidOperationException">Выбрасывается при ошибках выполнения SQL-запроса на уровне БД.</exception>
        public async Task ClearDatabaseAsync(CancellationToken cancellationToken = default)
        {

            _context.ChangeTracker.Clear();

            // Использование TRUNCATE CASCADE каскадно очищает зависимости и сбрасывает IDENTITY последовательности
            const string truncateSql = @"
                TRUNCATE TABLE cd_grade, cd_student, cd_group, cd_discipline, cd_specialty 
                RESTART IDENTITY CASCADE;";

            await _context.Database.ExecuteSqlRawAsync(truncateSql, cancellationToken);

        }

        /// <summary>
        /// Формирует и возвращает полный дамп данных всех таблиц системы.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Объект <see cref="DatabaseDumpDto"/>, содержащий списки записей всех сущностей.</returns>
        public async Task<DatabaseDumpDto> GetDatabaseDumpAsync(CancellationToken cancellationToken = default)
        {

            return new DatabaseDumpDto
            {

                Specialties = await _context.Specialties.AsNoTracking()
                    .Select(s => new SpecialtyDumpDto
                    {
                        SpecialtyId = s.SpecialtyId,
                        Title = s.Title,
                        Code = s.Code
                    })
                    .ToListAsync(cancellationToken),

                Groups = await _context.Groups.AsNoTracking()
                    .Select(g => new GroupDumpDto
                    {
                        GroupId = g.GroupId,
                        Name = g.Name,
                        Course = g.Course,
                        IsDeleted = g.IsDeleted,
                        SpecialtyId = g.SpecialtyId
                    })
                    .ToListAsync(cancellationToken),

                Students = await _context.Students.AsNoTracking()
                    .Select(s => new StudentDumpDto
                    {
                        StudentId = s.StudentId,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        IsDeleted = s.IsDeleted,
                        GroupId = s.GroupId
                    })
                    .ToListAsync(cancellationToken),

                Disciplines = await _context.Disciplines.AsNoTracking()
                    .Select(d => new DisciplineDumpDto
                    {
                        DisciplineId = d.DisciplineId,
                        Name = d.Name,
                        IsDeleted = d.IsDeleted
                    })
                    .ToListAsync(cancellationToken),

                Grades = await _context.Grades.AsNoTracking()
                    .Select(g => new GradeDumpDto
                    {
                        GradeId = g.GradeId,
                        Value = g.Value,
                        StudentId = g.StudentId,
                        DisciplineId = g.DisciplineId
                    })
                    .ToListAsync(cancellationToken)

            };

        }


        /// <summary>
        /// Атомарно восстанавливает состояние базы данных из переданного объекта дампа.
        /// </summary>
        /// <remarks>
        /// Операция выполняется в единой транзакции. В случае любой ошибки происходит полный откат транзакции.
        /// После вставки записей выполняется автоматическая синхронизация PostgreSQL-последовательностей.
        /// </remarks>
        /// <param name="dump">Объект дампа <see cref="DatabaseDumpDto"/> с восстанавливаемыми данными.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если переданный <paramref name="dump"/> равен <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">Выбрасывается при ошибках валидации данных или сбоях во время транзакции.</exception>
        public async Task RestoreDatabaseDumpAsync(DatabaseDumpDto dump, CancellationToken cancellationToken = default)
        {

            if (dump == null) throw new ArgumentNullException(nameof(dump), "Дамп базы данных не может быть null.");

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                // 1. Очищаем текущие данные и трекер EF Core
                await ClearDatabaseAsync(cancellationToken);

                // 2. Вставка в строго иерархическом порядке внешних ключей:

                // Специальности (Родительская сущность для Групп)
                if (dump.Specialties != null && dump.Specialties.Count > 0)
                {
                    var specialties = dump.Specialties.Select(s => new Specialty
                    {
                        SpecialtyId = s.SpecialtyId,
                        Title = s.Title,
                        Code = s.Code
                    });
                    await _context.Specialties.AddRangeAsync(specialties, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // Группы (Зависят от Специальностей)
                if (dump.Groups != null && dump.Groups.Count > 0)
                {
                    var groups = dump.Groups.Select(g => new Group
                    {
                        GroupId = g.GroupId,
                        Name = g.Name,
                        Course = g.Course,
                        IsDeleted = g.IsDeleted,
                        SpecialtyId = g.SpecialtyId
                    });
                    await _context.Groups.AddRangeAsync(groups, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // Студенты (Зависят от Групп)
                if (dump.Students != null && dump.Students.Count > 0)
                {
                    var students = dump.Students.Select(s => new Student
                    {
                        StudentId = s.StudentId,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        IsDeleted = s.IsDeleted,
                        GroupId = s.GroupId
                    });
                    await _context.Students.AddRangeAsync(students, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // Дисциплины (Независимая сущность)
                if (dump.Disciplines != null && dump.Disciplines.Count > 0)
                {
                    var disciplines = dump.Disciplines.Select(d => new Discipline
                    {
                        DisciplineId = d.DisciplineId,
                        Name = d.Name,
                        IsDeleted = d.IsDeleted
                    });
                    await _context.Disciplines.AddRangeAsync(disciplines, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // Оценки (Зависят от Студентов и Дисциплин)
                if (dump.Grades != null && dump.Grades.Count > 0)
                {
                    var grades = dump.Grades.Select(g => new Grade
                    {
                        GradeId = g.GradeId,
                        Value = g.Value,
                        StudentId = g.StudentId,
                        DisciplineId = g.DisciplineId
                    });
                    await _context.Grades.AddRangeAsync(grades, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                // 3. Синхронизируем счетчики автоинкремента PostgreSQL (Sequences)
                await ResetDatabaseSequencesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }

            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new InvalidOperationException($"Ошибка при восстановлении дампа базы данных: {ex.Message}", ex);
            }

        }


        /// <summary>
        /// Обновляет текущие значения генераторов последовательностей (Sequences) в PostgreSQL
        /// до максимальных значений первичных ключей в таблицах.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        private async Task ResetDatabaseSequencesAsync(CancellationToken cancellationToken)
        {

            const string syncSql = @"
                SELECT setval(pg_get_serial_sequence('cd_specialty', 'c_specialty_id'), GREATEST(COALESCE((SELECT MAX(c_specialty_id) FROM cd_specialty), 1), 1));
                SELECT setval(pg_get_serial_sequence('cd_group', 'c_group_id'), GREATEST(COALESCE((SELECT MAX(c_group_id) FROM cd_group), 1), 1));
                SELECT setval(pg_get_serial_sequence('cd_student', 'c_student_id'), GREATEST(COALESCE((SELECT MAX(c_student_id) FROM cd_student), 1), 1));
                SELECT setval(pg_get_serial_sequence('cd_discipline', 'c_discipline_id'), GREATEST(COALESCE((SELECT MAX(c_discipline_id) FROM cd_discipline), 1), 1));
                SELECT setval(pg_get_serial_sequence('cd_grade', 'c_grade_id'), GREATEST(COALESCE((SELECT MAX(c_grade_id) FROM cd_grade), 1), 1));
            ";

            await _context.Database.ExecuteSqlRawAsync(syncSql, cancellationToken);

        }

    }

}
