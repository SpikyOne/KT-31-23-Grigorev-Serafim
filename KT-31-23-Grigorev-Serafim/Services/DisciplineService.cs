using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.DTOs.Disciplines;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using Microsoft.EntityFrameworkCore;




namespace KT_31_23_Grigorev_Serafim.Services
{

    public class DisciplineService : IDisciplineService
    {

        private readonly AppDbContext _dbContext;


        public DisciplineService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<DisciplineResponse[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
        
            var query = _dbContext.Disciplines.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var nameLower = filter.Name.ToLower();
                query = query.Where(d => d.Name.ToLower().Contains(nameLower));
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(d => d.IsDeleted == filter.IsDeleted.Value);
            }

            var disciplines = await query.Select(d => new DisciplineResponse
            {
                DisciplineId = d.DisciplineId,
                Name = d.Name,
                IsDeleted = d.IsDeleted
            }).ToArrayAsync(cancellationToken);


            return disciplines;

        }

    }

}
