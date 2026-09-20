using Microsoft.AspNetCore.Mvc;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.DTOs.Students;




namespace KT_31_23_Grigorev_Serafim.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentService _studentService;


        // Внедрение зависимости через конструктор
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet("GetStudents", Name = "GetStudentsByFilter")]
        public async Task<IActionResult> GetStudentsByFilterAsync([FromQuery] StudentFilter filter, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsByFilterAsync(filter, cancellationToken);
            return Ok(students);
        }


        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudentAsync([FromBody] CreateStudentRequest request, CancellationToken cancellationToken)
        {
            var result = await _studentService.AddStudentAsync(request, cancellationToken);
            return Ok(result);
        }


        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _studentService.UpdateStudentAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudentAsync(int studentId, CancellationToken cancellationToken)
        {
            await _studentService.DeleteStudentAsync(studentId, cancellationToken);
            return Ok();
        }

    }

}
