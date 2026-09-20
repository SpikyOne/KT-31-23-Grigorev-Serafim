using Microsoft.AspNetCore.Mvc;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Filters;
using KT_31_23_Grigorev_Serafim.DTOs.Disciplines;




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


        [HttpPost("AddDiscipline")]
        public async Task<IActionResult> AddDisciplineAsync([FromBody] CreateDisciplineRequest request, CancellationToken cancellationToken)
        {
            var result = await _disciplineService.AddDisciplineAsync(request, cancellationToken);
            return Ok(result);
        }


        [HttpPut("UpdateDiscipline")]
        public async Task<IActionResult> UpdateDisciplineAsync([FromBody] UpdateDisciplineRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _disciplineService.UpdateDisciplineAsync(request, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpDelete("DeleteDiscipline")]
        public async Task<IActionResult> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken)
        {
            await _disciplineService.DeleteDisciplineAsync(disciplineId, cancellationToken);
            return Ok();
        }

    }

}
