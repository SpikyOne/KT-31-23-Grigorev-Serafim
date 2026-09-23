using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using Microsoft.AspNetCore.Mvc;




namespace KT_31_23_Grigorev_Serafim.Controllers
{

    /// <summary>
    /// Контроллер для получения информации о дисциплинах студентов
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Tags("Сервис 5. Дисциплины студентов")]
    public class StudentDisciplinesController : ControllerBase
    {

        private readonly IStudentDisciplinesService _studentDisciplinesService;


        /// <summary>
        /// Инициализирует новый экземпляр контроллера дисциплин студентов
        /// </summary>
        /// <param name="studentDisciplinesService">Сервис дисциплин студентов</param>
        public StudentDisciplinesController(IStudentDisciplinesService studentDisciplinesService)
        {
            _studentDisciplinesService = studentDisciplinesService;
        }


        /// <summary>
        /// Получение списка дисциплин, по которым у студента с указанной фамилией оценка 5
        /// </summary>
        /// <remarks>
        /// Возвращает массив названий дисциплин без повторов.
        /// </remarks>
        /// <param name="filter">Фильтр с фамилией студента</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Возвращает массив названий дисциплин</response>
        [HttpGet("GetExcellentDisciplines")]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExcellentDisciplinesByLastNameAsync([FromQuery] StudentLastNameFilter filter, CancellationToken cancellationToken)
        {
            var result = await _studentDisciplinesService.GetExcellentDisciplinesByLastNameAsync(filter, cancellationToken);
            return Ok(result);
        }

    }

}
