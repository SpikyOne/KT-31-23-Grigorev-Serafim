using Microsoft.AspNetCore.Mvc;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Filters;




namespace KT_31_23_Grigorev_Serafim.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DisciplinesController : ControllerBase
    {

        private readonly IDisciplineService _disciplineService;


        public DisciplinesController(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
        }


        [HttpGet("GetDisciplines", Name = "GetDisciplinesByFilter")]
        public async Task<IActionResult> GetDisciplinesByFilterAsync([FromQuery] DisciplineFilter filter, CancellationToken cancellationToken)
        {
            var disciplines = await _disciplineService.GetDisciplinesByFilterAsync(filter, cancellationToken);
            return Ok(disciplines);
        }

    }

}
