using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoApi.Data;
using ToDoApi.Entities;
using ToDoApi.Exceptions;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDoApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TaskService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<TaskResponseDto>> GetAllTasks(int userId)
        {
            try
            {
                if (!await _context.Users.AnyAsync(x => x.Id == userId))
                    throw new TaskValidationException("User not found.");

                if (!await _context.Tasks.AnyAsync(x => x.UserId == userId))
                    throw new TaskValidationException("No tasks found.");

                var tasks = await _context.Tasks
                    .Where(x => x.UserId == userId)
                    .Select(x => new TaskResponseDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Items = x.TaskItems.Select(item => new TaskItemResponseDto
                        {
                            Id = item.Id,
                            Title = item.Title,
                            Description = item.Description,
                            IsCompleted = item.IsCompleted,
                            DueDate = item.DueDate
                        }).ToList()
                    }).ToListAsync();

                return tasks;
            }
            catch (TaskValidationException ex)
            {
                throw new TaskValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<string> Create(TaskDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                    throw new TaskValidationException("O título da tarefa precisa estar preenchido.");

                if (await _context.Tasks.AnyAsync(x => x.Name.ToLower().Equals(dto.Name.ToLower())))
                    throw new TaskValidationException("Uma tarefa com esse título já existe.");

                var user = await GetUser();

                TaskList task = new TaskList
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    User = user,
                    UserId = user.Id
                };

                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();

                return "Tarefa criada com sucesso!";
            }
            catch (TaskValidationException ex)
            {
                throw new TaskValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                if (id < 0)
                    throw new TaskValidationException("ID informado não é válido.");

                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                    throw new TaskValidationException("Tarefa não encontrada.");

                if (task.TaskItems.Any())
                    _context.TaskItems.RemoveRange(task.TaskItems);

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return "Tarefa apagada com sucesso!";
            }
            catch (TaskValidationException ex)
            {
                throw new TaskValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Update(int id, TaskDto dto)
        {
            try
            {
                if (id < 0)
                    throw new TaskValidationException("ID informado não é válido.");

                if (string.IsNullOrEmpty(dto.Name))
                    throw new TaskValidationException("O título da tarefa precisa estar preenchido.");

                if (await _context.Tasks.AnyAsync(x => x.Name.ToLower().Equals(dto.Name.ToLower())))
                    throw new TaskValidationException("Uma tarefa com esse título já existe.");

                var task = await _context.Tasks.FindAsync(id);

                task.Name = dto.Name;
                task.Description = dto.Description;
                task.UpdateAt = DateTime.UtcNow;

                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();

                return "Tarefa atualizada com sucesso!";
            }
            catch (TaskValidationException ex)
            {
                throw new TaskValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApplicationUser> GetUser()
        {
            var userIdString = _httpContextAccessor?.HttpContext?.User?.FindFirstValue("userId");

            if (string.IsNullOrWhiteSpace(userIdString))
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            if (!int.TryParse(userIdString, out var userId))
                throw new Exception("Id do usuário inválido.");

            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("Usuário não encontrado.");

            return user;
        }
    }
}
