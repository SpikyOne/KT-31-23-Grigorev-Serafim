using KT_31_23_Grigorev_Serafim.DTOs.Grades;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.Interfaces;
using Microsoft.AspNetCore.Mvc;




namespace KT_31_23_Grigorev_Serafim.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
    
        private readonly IGradeService _gradeService;

        
        public GradesController(IGradeService gradeService)
        {
                _gradeService = gradeService;
        }

        
        // 1. Оценка у конкретного студента (и общая выборка по фильтрам)
        [HttpGet("GetGrades", Name = "GetGradesByFilter")]
        public async Task<IActionResult> GetGradesByFilterAsync([FromQuery] GradeFilter filter, CancellationToken cancellationToken)
        {

            var grades = await _gradeService.GetGradesByFilterAsync(filter, cancellationToken);
            return Ok(grades);

        }

        
        // 2. Средний балл по предмету в группе
        [HttpGet("AverageByGroupAndDiscipline")]
        public async Task<IActionResult> GetAverageByGroupAndDisciplineAsync(string groupName, string disciplineName, CancellationToken cancellationToken)
        {

            var average = await _gradeService.GetAverageGradeByGroupAndDisciplineAsync(groupName, disciplineName, cancellationToken);

            if (average.HasValue)
                return Ok(new { Group = groupName, Discipline = disciplineName, AverageGrade = Math.Round(average.Value, 2) });


            return NotFound("Оценки по заданным параметрам не найдены");

        }

        
        // 3. Средний балл по году (курсу)
        [HttpGet("AverageByCourse")]
        public async Task<IActionResult> GetAverageByCourseAsync(int course, CancellationToken cancellationToken)
        {

            var average = await _gradeService.GetAverageGradeByCourseAsync(course, cancellationToken);

            if (average.HasValue)
                return Ok(new { Course = course, AverageGrade = Math.Round(average.Value, 2) });


            return NotFound("Оценки для данного курса не найдены");

        }


        [HttpPost("AddGrade")]
        public async Task<IActionResult> AddGradeAsync([FromBody] CreateGradeRequest request, CancellationToken cancellationToken)
        {
            var result = await _gradeService.AddGradeAsync(request, cancellationToken);
            return Ok(result);
        }


        [HttpPut("UpdateGrade")]
        public async Task<IActionResult> UpdateGradeAsync([FromBody] UpdateGradeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _gradeService.UpdateGradeAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpDelete("DeleteGrade")]
        public async Task<IActionResult> DeleteGradeAsync(int gradeId, CancellationToken cancellationToken)
        {
            await _gradeService.DeleteGradeAsync(gradeId, cancellationToken);
            return Ok();
        }

    }

}
