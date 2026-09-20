using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.DTOs.Grades;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Services
{

    public class GradeService : IGradeService
    {

        private readonly AppDbContext _dbContext;


        public GradeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<GradeResponse[]> GetGradesByFilterAsync(GradeFilter filter, CancellationToken cancellationToken = default)
        {
        
            var query = _dbContext.Grades.AsQueryable();

            if (filter.StudentId.HasValue)
                query = query.Where(g => g.StudentId == filter.StudentId.Value);

            if (!string.IsNullOrEmpty(filter.GroupName))
                query = query.Where(g => g.Student.Group.Name == filter.GroupName);

            if (!string.IsNullOrEmpty(filter.DisciplineName))
                query = query.Where(g => g.Discipline.Name == filter.DisciplineName);

            if (filter.Course.HasValue)
                query = query.Where(g => g.Student.Group.Course == filter.Course.Value);

            var grades = await query.Select(g => new GradeResponse
            {
                GradeId = g.GradeId,
                Value = g.Value,
                StudentName = $"{g.Student.LastName} {g.Student.FirstName}",
                DisciplineName = g.Discipline.Name,
                GroupName = g.Student.Group.Name,
                Course = g.Student.Group.Course
            }).ToArrayAsync(cancellationToken);


            return grades;

        }


        public async Task<double?> GetAverageGradeByGroupAndDisciplineAsync(string groupName, string disciplineName, CancellationToken cancellationToken = default)
        {

            var query = _dbContext.Grades
                .Where(g => g.Student.Group.Name == groupName && g.Discipline.Name == disciplineName);

            if (!await query.AnyAsync(cancellationToken))
                return null;


            return await query.AverageAsync(g => (double)g.Value, cancellationToken);

        }


        public async Task<double?> GetAverageGradeByCourseAsync(int course, CancellationToken cancellationToken = default)
        {
            
            var query = _dbContext.Grades
                .Where(g => g.Student.Group.Course == course);

            if (!await query.AnyAsync(cancellationToken))
                return null;


            return await query.AverageAsync(g => (double)g.Value, cancellationToken);

        }

    }

}
