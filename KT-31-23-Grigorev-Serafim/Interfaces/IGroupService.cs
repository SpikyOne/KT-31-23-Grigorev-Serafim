using KT_31_23_Grigorev_Serafim.DTOs.Groups;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Models;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{
    public interface IGroupService
    {
        Task<GroupResponse[]> GetGroupsByFilterAsync(GroupFilter filter, CancellationToken cancellationToken);


        Task DeleteGroupAsync(int groupId, CancellationToken cancellationToken);
        // TODO: Сюда же ДОБАВИТЬ методы Add и Update
    }
}

