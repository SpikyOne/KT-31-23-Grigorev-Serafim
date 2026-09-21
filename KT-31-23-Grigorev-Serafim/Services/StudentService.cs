using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.DTOs.Students;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Models;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Services
{

    /// <summary>
    /// Сервис для управления студентами
    /// </summary>
    public class StudentService : IStudentService
    {

        private readonly AppDbContext _dbContext;


        /// <summary>
        /// Инициализирует новый экземпляр сервиса студентов
        /// </summary>
        /// <param name="dbContext">Контекст базы данных</param>
        public StudentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <inheritdoc />
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


        /// <inheritdoc />
        public async Task<StudentResponse> AddStudentAsync(CreateStudentRequest request, CancellationToken cancellationToken = default)
        {

            var student = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                GroupId = request.GroupId,
                IsDeleted = false
            };

            await _dbContext.Students.AddAsync(student, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Подгружаем группу для корректного формирования ответа с именем группы
            await _dbContext.Entry(student).Reference(s => s.Group).LoadAsync(cancellationToken);


            return new StudentResponse
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                GroupName = student.Group.Name,
                IsDeleted = student.IsDeleted
            };

        }


        /// <inheritdoc />
        public async Task<StudentResponse> UpdateStudentAsync(UpdateStudentRequest request, CancellationToken cancellationToken = default)
        {

            var student = await _dbContext.Students
                .Include(s => s.Group)
                .FirstOrDefaultAsync(s => s.StudentId == request.StudentId, cancellationToken);

            if (student == null) throw new Exception("Студент не найден");

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.GroupId = request.GroupId;
            student.IsDeleted = request.IsDeleted;

            await _dbContext.SaveChangesAsync(cancellationToken);

            // Если группа изменилась, подгружаем новые данные
            await _dbContext.Entry(student).Reference(s => s.Group).LoadAsync(cancellationToken);


            return new StudentResponse
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                GroupName = student.Group.Name,
                IsDeleted = student.IsDeleted
            };

        }


        /// <inheritdoc />
        public async Task DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
        {

            var student = await _dbContext.Students.FirstOrDefaultAsync(s => s.StudentId == studentId, cancellationToken);

            if (student != null)
            {
                // Логическое удаление студента
                student.IsDeleted = true;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

        }

    }

}
