using KT_31_23_Grigorev_Serafim.DTOs.Grades;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using Microsoft.AspNetCore.Mvc;




namespace KT_31_23_Grigorev_Serafim.Controllers
{

    /// <summary>
    /// Контроллер управления успеваемостью студентов
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Tags("Сервис 4. Управление успеваемостью")] // <--- Группировка в Swagger
    public class GradesController : ControllerBase
    {
    
        private readonly IGradeService _gradeService;


        /// <summary>
        /// Инициализирует новый экземпляр контроллера оценок
        /// </summary>
        /// <param name="gradeService">Сервис работы с оценками</param>
        public GradesController(IGradeService gradeService)
        {
                _gradeService = gradeService;
        }


        // 1. Оценка у конкретного студента (и общая выборка по фильтрам)
        /// <summary>
        /// Получение журнала оценок по фильтрам
        /// </summary>
        /// <remarks>
        /// Позволяет получить оценки конкретного студента, отдельной группы, по конкретной дисциплине или курсу в целом.
        /// </remarks>
        /// <param name="filter">Модель фильтрации успеваемости</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Возвращает массив найденных оценок</response>
        [HttpGet("GetGrades", Name = "GetGradesByFilter")]
        [ProducesResponseType(typeof(GradeResponse[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGradesByFilterAsync([FromQuery] GradeFilter filter, CancellationToken cancellationToken)
        {

            var grades = await _gradeService.GetGradesByFilterAsync(filter, cancellationToken);
            return Ok(grades);

        }


        // 2. Средний балл по предмету в группе
        /// <summary>
        /// Расчет среднего балла группы по конкретной дисциплине
        /// </summary>
        /// <param name="groupName">Точное название группы (например, "ИВТ-1-23")</param>
        /// <param name="disciplineName">Точное название дисциплины</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Успешный расчет среднего балла</response>
        /// <response code="404">Оценки по заданным параметрам не найдены</response>
        [HttpGet("AverageByGroupAndDiscipline")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAverageByGroupAndDisciplineAsync(string groupName, string disciplineName, CancellationToken cancellationToken)
        {

            var average = await _gradeService.GetAverageGradeByGroupAndDisciplineAsync(groupName, disciplineName, cancellationToken);

            if (average.HasValue)
                return Ok(new { Group = groupName, Discipline = disciplineName, AverageGrade = Math.Round(average.Value, 2) });


            return NotFound("Оценки по заданным параметрам не найдены");

        }


        // 3. Средний балл по году (курсу)
        /// <summary>
        /// Расчет среднего балла по всему курсу
        /// </summary>
        /// <param name="course">Номер курса (число)</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Успешный расчет среднего балла на потоке</response>
        /// <response code="404">Оценки для данного курса не найдены</response>
        [HttpGet("AverageByCourse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAverageByCourseAsync(int course, CancellationToken cancellationToken)
        {

            var average = await _gradeService.GetAverageGradeByCourseAsync(course, cancellationToken);

            if (average.HasValue)
                return Ok(new { Course = course, AverageGrade = Math.Round(average.Value, 2) });


            return NotFound("Оценки для данного курса не найдены");

        }


        /// <summary>
        /// Выставление новой оценки студенту
        /// </summary>
        /// <param name="request">Данные об оценке, студенте и дисциплине</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Оценка успешно добавлена в журнал</response>
        [HttpPost("AddGrade")]
        [ProducesResponseType(typeof(GradeResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddGradeAsync([FromBody] CreateGradeRequest request, CancellationToken cancellationToken)
        {
            var result = await _gradeService.AddGradeAsync(request, cancellationToken);
            return Ok(result);
        }


        /// <summary>
        /// Изменение существующей оценки
        /// </summary>
        /// <param name="request">Новые данные оценки</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Оценка успешно изменена</response>
        /// <response code="400">Оценка с указанным ID не найдена</response>
        [HttpPut("UpdateGrade")]
        [ProducesResponseType(typeof(GradeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateGradeAsync([FromBody] UpdateGradeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _gradeService.UpdateGradeAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        /// <summary>
        /// Физическое удаление оценки из журнала
        /// </summary>
        /// <remarks>
        /// Внимание: запись удаляется из базы данных безвозвратно.
        /// </remarks>
        /// <param name="gradeId">Идентификатор удаляемой оценки</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Оценка успешно удалена</response>
        [HttpDelete("DeleteGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteGradeAsync(int gradeId, CancellationToken cancellationToken)
        {
            await _gradeService.DeleteGradeAsync(gradeId, cancellationToken);
            return Ok();
        }

    }

}
