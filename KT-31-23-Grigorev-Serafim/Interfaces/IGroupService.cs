using KT_31_23_Grigorev_Serafim.DTOs.Groups;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Models;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{

    /// <summary>
    /// Интерфейс сервиса для управления учебными группами
    /// </summary>
    public interface IGroupService
    {

        /// <summary>
        /// Получение списка групп по фильтру
        /// </summary>
        /// <param name="filter">Параметры фильтрации групп</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Массив найденных групп</returns>
        Task<GroupResponse[]> GetGroupsByFilterAsync(GroupFilter filter, CancellationToken cancellationToken);


        /// <summary>
        /// Создание новой группы
        /// </summary>
        Task<GroupResponse> AddGroupAsync(CreateGroupRequest request, CancellationToken cancellationToken = default);


        /// <summary>
        /// Обновление данных группы
        /// </summary>
        Task<GroupResponse> UpdateGroupAsync(UpdateGroupRequest request, CancellationToken cancellationToken = default);


        /// <summary>
        /// Логическое удаление группы и связанных студентов
        /// </summary>
        Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default);

    }

}
