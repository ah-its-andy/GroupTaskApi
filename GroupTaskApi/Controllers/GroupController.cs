using GroupTaskApi.DomainModel;
using GroupTaskApi.Filter;
using GroupTaskApi.Service;
using GroupTaskApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace GroupTaskApi.Controllers
{
    [Identity]
    public class GroupController : Controller
    {
        private readonly GroupService _groupService;
        private readonly TaskService _taskService;

        public GroupController(GroupService groupService, TaskService taskService)
        {
            _groupService = groupService;
            _taskService = taskService;
        }

        [HttpPost("group")]
        public async Task<IActionResult> Post([FromBody] CreateGroupViewModel viewModel)
        {
            if (!HttpContext.Items.ContainsKey("identity_claims")) return NoContent();
            var userId = ((JObject)HttpContext.Items["identity_claims"])["sub"].Value<long>();
            viewModel.OwnerId = userId;
            await _groupService.CreateGroupAsync(viewModel);
            return Ok();
        }

        [HttpGet("group")]
        public async Task<IActionResult> GetAllAsync()
        {
            if (!HttpContext.Items.ContainsKey("identity_claims")) return NoContent();
            var userId = ((JObject)HttpContext.Items["identity_claims"])["sub"].Value<long>();
            return Ok(await _groupService.GetGroupListAsync(userId));
        }

        [HttpGet("group/{code}")]
        public async Task<IActionResult> GetOneAsync(string code)
        {
            return Ok(await _groupService.FindByCodeAsync(code));
        }

        [HttpPut("group/{groupId}/join")]
        public async Task<IActionResult> JoinGroupAsync(long groupId)
        {
            if (!HttpContext.Items.ContainsKey("identity_claims")) return NoContent();
            var userId = ((JObject)HttpContext.Items["identity_claims"])["sub"].Value<long>();

            await _groupService.JoinGroupAsync(new JoinGroupViewModel
            {
                GroupId = groupId,
                UserId = userId
            });
            return Ok();
        }

        [HttpGet("/group/{groupId}/task")]
        public async Task<IActionResult> GetTaskListAsync(long groupId)
        {
            return Ok(await _taskService.GetTaskList(groupId));
        }

        [HttpPost("/group/{groupId}/task")]
        public async Task<IActionResult> CreateTaskAsync(long groupId, [FromBody]CreateTaskViewModel viewModel)
        {
            if (!HttpContext.Items.ContainsKey("identity_claims")) return NoContent();
            var userId = ((JObject)HttpContext.Items["identity_claims"])["sub"].Value<long>();

            viewModel.GroupId = groupId;
            viewModel.OwnerId = userId;
            await _taskService.CreateTaskAsync(viewModel);
            return Ok();
        }

        [HttpPost("/group/{groupId}/task/{taskId}/complete")]
        public async Task<IActionResult> SetTaskCompletedAsync(long groupId, long taskId)
        {
            await _taskService.ChangeStateAsync(new ChangeTaskStateViewModel()
            {
                GroupId = groupId,
                TaskId = taskId,
                State = GroupModel.TaskState.Completed
            });
            return Ok();
        }
    }
}
