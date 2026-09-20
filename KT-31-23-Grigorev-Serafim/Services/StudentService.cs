using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.DTOs.Students;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Services
{

    public class StudentService : IStudentService
    {

        private readonly AppDbContext _dbContext;


        public StudentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<StudentResponse[]> GetStudentsByFilterAsync(StudentFilter filter, CancellationToken cancellationToken = default)
        {

            var query = _dbContext.Students.AsQueryable();

            if (!string.IsNullOrEmpty(filter.GroupName))
                query = query.Where(s => s.Group.Name == filter.GroupName);

            if (!string.IsNullOrEmpty(filter.FIO))
            {
                var fio = filter.FIO.ToLower();
                // Фильтрация по склейке имени и фамилии в любом порядке
                query = query.Where(s => (s.LastName + " " + s.FirstName).ToLower().Contains(fio)
                                      || (s.FirstName + " " + s.LastName).ToLower().Contains(fio));
            }

            if (filter.IsDeleted.HasValue)
                query = query.Where(s => s.IsDeleted == filter.IsDeleted.Value);

            var students = await query.Select(s => new StudentResponse
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                GroupName = s.Group.Name, // EF Core автоматически сделает JOIN таблицы Group
                IsDeleted = s.IsDeleted
            }).ToArrayAsync(cancellationToken);

            return students;

        }

    }

}
