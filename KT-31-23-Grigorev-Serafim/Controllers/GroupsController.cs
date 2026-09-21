using Microsoft.AspNetCore.Mvc;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.DTOs.Groups;




namespace KT_31_23_Grigorev_Serafim.Controllers
{

    /// <summary>
    /// Контроллер управления учебными группами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Tags("Сервис 1. Управление группами")] // <--- Группировка в Swagger
    public class GroupsController : ControllerBase
    {

        private readonly IGroupService _groupService;


        /// <summary>
        /// Инициализирует новый экземпляр контроллера групп
        /// </summary>
        /// <param name="groupService">Сервис работы с группами</param>
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }


        /// <summary>
        /// Получение списка групп по фильтру
        /// </summary>
        /// <remarks>
        /// Позволяет получить массив групп. Если фильтры не переданы, возвращаются все группы из БД.
        /// </remarks>
        /// <param name="filter">Объект фильтрации (специальность, курс, статус)</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Успешное получение списка групп</response>
        [HttpGet("GetGroups", Name = "GetGroupsByFilter")]
        [ProducesResponseType(typeof(GroupResponse[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupsByFilterAsync([FromQuery] GroupFilter filter, CancellationToken cancellationToken)
        {
            var groups = await _groupService.GetGroupsByFilterAsync(filter, cancellationToken);
            return Ok(groups);
        }


        /// <summary>
        /// Создание новой учебной группы
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Groups/AddGroup
        ///     {
        ///        "name": "ИВТ-1-23",
        ///        "course": 1,
        ///        "specialtyId": 1
        ///     }
        ///     
        /// </remarks>
        /// <param name="request">Данные новой группы</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Группа успешно создана</response>
        [HttpPost("AddGroup")]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddGroupAsync([FromBody] CreateGroupRequest request, CancellationToken cancellationToken)
        {
            var result = await _groupService.AddGroupAsync(request, cancellationToken);
            return Ok(result);
        }


        /// <summary>
        /// Обновление данных существующей группы
        /// </summary>
        /// <param name="request">Новые данные группы (включая ID)</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Группа успешно обновлена</response>
        /// <response code="400">Передан неверный идентификатор (группа не найдена)</response>
        [HttpPut("UpdateGroup")]
        [ProducesResponseType(typeof(GroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateGroupAsync([FromBody] UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _groupService.UpdateGroupAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        /// <summary>
        /// Логическое удаление группы и связанных студентов
        /// </summary>
        /// <remarks>
        /// Помечает группу как удаленную (IsDeleted = true). 
        /// Также каскадно выполняет логическое удаление всех студентов, состоящих в этой группе.
        /// </remarks>
        /// <param name="groupId">Идентификатор удаляемой группы</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Группа и студенты успешно удалены</response>
        [HttpDelete("DeleteGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteGroupAsync(int groupId, CancellationToken cancellationToken)
        {
            await _groupService.DeleteGroupAsync(groupId, cancellationToken);
            return Ok();
        }

    }

}
