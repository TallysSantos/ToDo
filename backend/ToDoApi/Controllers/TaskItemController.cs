using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Exceptions;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/v1/taskList")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpPost("{id}/items")]
        public async Task<IActionResult> CreateTaskItem(int id, [FromBody] TaskItemDto dto)
        {
            try
            {
                var taskItem = await _taskItemService.Create(id, dto);

                return Ok(new { Message = taskItem });
            }
            catch (TaskItemValidationException ex)
            {
                return BadRequest(new { Message = new { Error = ex.Message } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = new { Error = ex.Message } });
            }

        }
    }
}
