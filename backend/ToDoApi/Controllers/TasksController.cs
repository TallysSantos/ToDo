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
    [Route("Api/v1/Tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId")?.Value);

                var tasks = await _taskService.GetAllTasks(userId);

                return StatusCode(200, new { Message = new { tasks } });
            }
            catch (TaskValidationException ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskDto dto)
        {
            try
            {
                string returnMessage = await _taskService.Create(dto);

                return StatusCode(201, new { Message = returnMessage });
            }
            catch (TaskValidationException ex)
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
                string returnMessage = await _taskService.Delete(id);

                return StatusCode(200, new { Message = returnMessage });
            }
            catch (TaskValidationException ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { Message = new { Error = ex.Message } });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskDto dto)
        {
            try
            {
                string returnMessage = await _taskService.Update(id, dto);

                return StatusCode(200, new { Message = returnMessage });
            }
            catch (TaskValidationException ex)
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