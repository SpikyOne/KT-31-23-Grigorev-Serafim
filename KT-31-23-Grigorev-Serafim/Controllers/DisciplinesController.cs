using Microsoft.AspNetCore.Mvc;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.DTOs.Disciplines;




namespace KT_31_23_Grigorev_Serafim.Controllers
{

    /// <summary>
    /// Контроллер управления учебными дисциплинами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Tags("Сервис 3. Управление дисциплинами")] // <--- Группировка в Swagger
    public class DisciplinesController : ControllerBase
    {

        private readonly IDisciplineService _disciplineService;


        /// <summary>
        /// Инициализирует новый экземпляр контроллера дисциплин
        /// </summary>
        /// <param name="disciplineService">Сервис работы с дисциплинами</param>
        public DisciplinesController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }


        /// <summary>
        /// Получение списка дисциплин по фильтру
        /// </summary>
        /// <remarks>
        /// Позволяет найти дисциплины по частичному совпадению названия или статусу удаления.
        /// </remarks>
        /// <param name="filter">Модель фильтрации</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Возвращает массив найденных дисциплин</response>
        [HttpGet("GetDisciplines", Name = "GetDisciplinesByFilter")]
        [ProducesResponseType(typeof(DisciplineResponse[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDisciplinesByFilterAsync([FromQuery] DisciplineFilter filter, CancellationToken cancellationToken)
        {
            var disciplines = await _disciplineService.GetDisciplinesByFilterAsync(filter, cancellationToken);
            return Ok(disciplines);
        }


        /// <summary>
        /// Добавление новой учебной дисциплины
        /// </summary>
        /// <param name="request">Данные для создания дисциплины</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Дисциплина успешно создана</response>
        [HttpPost("AddDiscipline")]
        [ProducesResponseType(typeof(DisciplineResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddDisciplineAsync([FromBody] CreateDisciplineRequest request, CancellationToken cancellationToken)
        {
            var result = await _disciplineService.AddDisciplineAsync(request, cancellationToken);
            return Ok(result);
        }


        /// <summary>
        /// Обновление названия или статуса дисциплины
        /// </summary>
        /// <param name="request">Обновленные данные дисциплины</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Данные успешно обновлены</response>
        /// <response code="400">Дисциплина с указанным ID не найдена</response>
        [HttpPut("UpdateDiscipline")]
        [ProducesResponseType(typeof(DisciplineResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDisciplineAsync([FromBody] UpdateDisciplineRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _disciplineService.UpdateDisciplineAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        /// <summary>
        /// Логическое удаление дисциплины
        /// </summary>
        /// <remarks>
        /// Помечает дисциплину как удаленную (IsDeleted = true), не удаляя ее физически из базы.
        /// </remarks>
        /// <param name="disciplineId">Идентификатор дисциплины</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Дисциплина успешно удалена</response>
        [HttpDelete("DeleteDiscipline")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken)
        {
            await _disciplineService.DeleteDisciplineAsync(disciplineId, cancellationToken);
            return Ok();
        }

    }

}
