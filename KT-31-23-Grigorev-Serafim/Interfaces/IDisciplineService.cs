using KT_31_23_Grigorev_Serafim.DTOs.Disciplines;
using KT_31_23_Grigorev_Serafim.Filters;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{

    /// <summary>
    /// Интерфейс сервиса для управления учебными дисциплинами
    /// </summary>
    public interface IDisciplineService
    {

        /// <summary>
        /// Получение списка дисциплин по фильтру
        /// </summary>
        Task<DisciplineResponse[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавление новой учебной дисциплины
        /// </summary>
        Task<DisciplineResponse> AddDisciplineAsync(CreateDisciplineRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновление данных учебной дисциплины
        /// </summary>
        Task<DisciplineResponse> UpdateDisciplineAsync(UpdateDisciplineRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Логическое удаление дисциплины
        /// </summary>
        Task DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default);

    }

}
