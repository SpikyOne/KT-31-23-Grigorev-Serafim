using Microsoft.AspNetCore.Mvc;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.DTOs.Students;




namespace KT_31_23_Grigorev_Serafim.Controllers
{

    /// <summary>
    /// Контроллер управления студентами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Tags("Сервис 2. Управление студентами")] // <--- Группировка в Swagger
    public class StudentsController : ControllerBase
    {

        private readonly IStudentService _studentService;


        // Внедрение зависимости через конструктор
        /// <summary>
        /// Инициализирует новый экземпляр контроллера студентов
        /// </summary>
        /// <param name="studentService">Сервис работы со студентами</param>
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        /// <summary>
        /// Получение списка студентов по фильтру
        /// </summary>
        /// <remarks>
        /// Позволяет отфильтровать студентов по названию их группы, ФИО или статусу удаления.
        /// Поиск по ФИО работает независимо от порядка (например, "Иван Иванов" и "Иванов Иван" найдут одну запись).
        /// </remarks>
        /// <param name="filter">Модель фильтрации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Возвращает массив студентов</response>
        [HttpGet("GetStudents", Name = "GetStudentsByFilter")]
        [ProducesResponseType(typeof(StudentResponse[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentsByFilterAsync([FromQuery] StudentFilter filter, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsByFilterAsync(filter, cancellationToken);
            return Ok(students);
        }


        /// <summary>
        /// Добавление нового студента
        /// </summary>
        /// <param name="request">Данные студента (имя, фамилия, ID группы)</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Студент успешно создан</response>
        [HttpPost("AddStudent")]
        [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddStudentAsync([FromBody] CreateStudentRequest request, CancellationToken cancellationToken)
        {
            var result = await _studentService.AddStudentAsync(request, cancellationToken);
            return Ok(result);
        }


        /// <summary>
        /// Обновление данных студента
        /// </summary>
        /// <param name="request">Обновленные данные студента</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Данные студента обновлены</response>
        /// <response code="400">Студент не найден в базе данных</response>
        [HttpPut("UpdateStudent")]
        [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _studentService.UpdateStudentAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        /// <summary>
        /// Логическое удаление студента
        /// </summary>
        /// <remarks>
        /// Переводит статус IsDeleted студента в состояние true. Физически из БД запись не удаляется.
        /// </remarks>
        /// <param name="studentId">Идентификатор удаляемого студента</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Успешное выполнение операции</response>
        [HttpDelete("DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            await _studentService.DeleteStudentAsync(studentId, cancellationToken);
            return Ok();
        }

    }

}
