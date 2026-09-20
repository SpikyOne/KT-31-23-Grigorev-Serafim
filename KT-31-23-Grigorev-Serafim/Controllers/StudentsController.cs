using Microsoft.AspNetCore.Mvc;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Filters;




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

    }

}
