using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoApi.Exceptions;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/v1/TaskLists")]
    public class TaskListsController : ControllerBase
    {
        private readonly ITaskListService _taskListService;
        public TaskListsController(ITaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId")?.Value);

                var taskLists = await _taskListService.GetAllTaskLists(userId);

                return StatusCode(200, new { Message = new { taskLists } });
            }
            catch (TaskListValidationException ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskList(TaskListDto dto)
        {
            try
            {
                string returnMessage = await _taskListService.Create(dto);

                return StatusCode(201, new { Message = returnMessage });
            }
            catch (TaskListValidationException ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string returnMessage = await _taskListService.Delete(id);

                return StatusCode(200, new { Message = returnMessage });
            }
            catch (TaskListValidationException ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskListDto dto)
        {
            try
            {
                string returnMessage = await _taskListService.Update(id, dto);

                return StatusCode(200, new { Message = returnMessage });
            }
            catch (TaskListValidationException ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
        }
    }
}