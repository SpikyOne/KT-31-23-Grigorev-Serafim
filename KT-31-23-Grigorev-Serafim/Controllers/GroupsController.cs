using Microsoft.AspNetCore.Mvc;
using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Filters;




namespace KT_31_23_Grigorev_Serafim.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {

        private readonly IGroupService _groupService;


        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }


        [HttpGet("GetGroups", Name = "GetGroupsByFilter")]
        public async Task<IActionResult> GetGroupsByFilterAsync([FromQuery] GroupFilter filter, CancellationToken cancellationToken)
        {
            var groups = await _groupService.GetGroupsByFilterAsync(filter, cancellationToken);
            return Ok(groups);
        }


        [HttpDelete("DeleteGroup")]
        public async Task<IActionResult> DeleteGroupAsync(int groupId, CancellationToken cancellationToken)
        {
            await _groupService.DeleteGroupAsync(groupId, cancellationToken);
            return Ok();
        }

    }

}