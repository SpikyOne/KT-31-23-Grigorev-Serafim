using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.DTOs.Groups;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Models;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Services
{

    /// <summary>
    /// Сервис для управления учебными группами
    /// </summary>
    public class GroupService : IGroupService
    {

        private readonly AppDbContext _dbContext;


        /// <summary>
        /// Инициализирует новый экземпляр сервиса групп
        /// </summary>
        /// <param name="dbContext">Контекст базы данных</param>
        public GroupService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <inheritdoc />
        public async Task<GroupResponse[]> GetGroupsByFilterAsync(GroupFilter filter, CancellationToken cancellationToken = default)
        {

            var query = _dbContext.Groups.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SpecialtyName))
                query = query.Where(g => g.Specialty.Title.Contains(filter.SpecialtyName));

            if (filter.Course.HasValue)
                query = query.Where(g => g.Course == filter.Course.Value);

            if (filter.IsDeleted.HasValue)
                query = query.Where(g => g.IsDeleted == filter.IsDeleted.Value);

            //5 Select для трансформации данных из БД в DTO
            var groups = await query.Select(g => new GroupResponse
            {
                GroupId = g.GroupId,
                Name = g.Name,
                Course = g.Course,
                SpecialtyName = g.Specialty.Title // EF Core сам сделает JOIN нужной таблицы
            }).ToArrayAsync(cancellationToken);


            return groups;

        }


        /// <inheritdoc />
        public async Task<GroupResponse> AddGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken = default)
        {

            var group = new Group
            {
                Name = request.Name,
                Course = request.Course,
                SpecialtyId = request.SpecialtyId,
                IsDeleted = false
            };

            await _dbContext.Groups.AddAsync(group, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Подгружаем специальность для корректного формирования ответа
            await _dbContext.Entry(group).Reference(g => g.Specialty).LoadAsync(cancellationToken);


            return new GroupResponse
            {
                GroupId = group.GroupId,
                Name = group.Name,
                Course = group.Course,
                SpecialtyName = group.Specialty.Title
            };

        }


        /// <inheritdoc />
        public async Task<GroupResponse> UpdateGroupAsync(UpdateGroupRequest request, CancellationToken cancellationToken = default)
        {

            var group = await _dbContext.Groups
                .Include(g => g.Specialty)
                .FirstOrDefaultAsync(g => g.GroupId == request.GroupId, cancellationToken);

            if (group == null) throw new Exception("Группа не найдена");

            group.Name = request.Name;
            group.Course = request.Course;
            group.SpecialtyId = request.SpecialtyId;
            group.IsDeleted = request.IsDeleted;

            await _dbContext.SaveChangesAsync(cancellationToken);

            // Если специальность изменилась, подгружаем новые данные
            await _dbContext.Entry(group).Reference(g => g.Specialty).LoadAsync(cancellationToken);


            return new GroupResponse
            {
                GroupId = group.GroupId,
                Name = group.Name,
                Course = group.Course,
                SpecialtyName = group.Specialty.Title
            };

        }


        /// <inheritdoc />
        public async Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default)
        {

            var group = await _dbContext.Groups.FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);

            if (group != null)
            {

                // Логическое удаление группы
                group.IsDeleted = true;

                // Поиск всех студентов этой группы и их логическое удаление
                var students = await _dbContext.Students
                    .Where(s => s.GroupId == groupId)
                    .ToListAsync(cancellationToken);

                foreach (var student in students) student.IsDeleted = true;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

        }

    }

}
