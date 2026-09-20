using KT_31_23_Grigorev_Serafim.DTOs.Disciplines;
using KT_31_23_Grigorev_Serafim.Filters;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{

    public interface IDisciplineService
    {
        Task<DisciplineResponse[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken = default);
    }

}
