using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.DTOs.Groups;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Models;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Services
{

    public class GroupService : IGroupService
    {

        private readonly AppDbContext _dbContext;


        public GroupService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


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


        public async Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);
            if (group != null)
            {
                // Если подразумевается физическое удаление:
                _dbContext.Groups.Remove(group);
                // Благодаря OnDelete(DeleteBehavior.Cascade) в GroupConfiguration, студенты удалятся автоматически

                // Если подразумевается логическое удаление (Soft Delete):
                // group.IsDeleted = true;
                // var students = await _dbContext.Students.Where(s => s.GroupId == groupId).ToListAsync();
                // students.ForEach(s => s.IsDeleted = true);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }

}