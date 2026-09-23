using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Services
{

    /// <summary>
    /// Сервис получения сведений о дисциплинах студента
    /// </summary>
    public class StudentDisciplinesService : IStudentDisciplinesService
    {

        private readonly AppDbContext _dbContext;


        /// <summary>
        /// Инициализирует новый экземпляр сервиса дисциплин студента
        /// </summary>
        /// <param name="dbContext">Контекст базы данных</param>
        public StudentDisciplinesService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <inheritdoc />
        public async Task<string[]> GetExcellentDisciplinesByLastNameAsync(StudentLastNameFilter filter, CancellationToken cancellationToken = default)
        {

            if (string.IsNullOrWhiteSpace(filter.LastName))
            {
                return Array.Empty<string>();
            }

            var lastNameLower = filter.LastName.Trim().ToLower();

            // Выборка всех дисциплин, где у студента с указанной фамилией оценка 5,
            // с исключением удаленных записей и исключением дубликатов (Distinct)
            var disciplines = await _dbContext.Grades
                .Where(g => g.Value == 5
                         && g.Student.LastName.ToLower() == lastNameLower
                         && !g.Student.IsDeleted
                         && !g.Discipline.IsDeleted)
                .Select(g => g.Discipline.Name)
                .Distinct()
                .OrderBy(name => name)
                .ToArrayAsync(cancellationToken);


            return disciplines;

        }

    }

}