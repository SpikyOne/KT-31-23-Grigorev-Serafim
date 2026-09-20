using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.DTOs.Disciplines;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Models;
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


        public async Task<DisciplineResponse> AddDisciplineAsync(CreateDisciplineRequest request, CancellationToken cancellationToken = default)
        {

            var discipline = new Discipline
            {
                Name = request.Name,
                IsDeleted = false
            };

            await _dbContext.Disciplines.AddAsync(discipline, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);


            return new DisciplineResponse
            {
                DisciplineId = discipline.DisciplineId,
                Name = discipline.Name,
                IsDeleted = discipline.IsDeleted
            };

        }


        public async Task<DisciplineResponse> UpdateDisciplineAsync(UpdateDisciplineRequest request, CancellationToken cancellationToken = default)
        {

            var discipline = await _dbContext.Disciplines
                .FirstOrDefaultAsync(d => d.DisciplineId == request.DisciplineId, cancellationToken);

            if (discipline == null) throw new Exception("Дисциплина не найдена");

            discipline.Name = request.Name;
            discipline.IsDeleted = request.IsDeleted;

            await _dbContext.SaveChangesAsync(cancellationToken);

            
            return new DisciplineResponse
            {
                DisciplineId = discipline.DisciplineId,
                Name = discipline.Name,
                IsDeleted = discipline.IsDeleted
            };

        }


        public async Task DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
        {

            var discipline = await _dbContext.Disciplines
                .FirstOrDefaultAsync(d => d.DisciplineId == disciplineId, cancellationToken);

            if (discipline != null)
            {
                // Логическое удаление
                discipline.IsDeleted = true;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

        }

    }

}
