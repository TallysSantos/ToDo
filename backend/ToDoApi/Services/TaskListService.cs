using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoApi.Data;
using ToDoApi.Entities;
using ToDoApi.Exceptions;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class TaskListService : ITaskListService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TaskListService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<TaskListResponseDto>> GetAllTaskLists(int userId)
        {
            try
            {
                if (!await _context.Users.AnyAsync(x => x.Id == userId))
                    throw new TaskListValidationException("User not found.");

                if (!await _context.TaskLists.AnyAsync(x => x.UserId == userId))
                    throw new TaskListValidationException("List not found.");

                var tasks = await _context.TaskLists
                    .Where(x => x.UserId == userId)
                    .Select(x => new TaskListResponseDto
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
            catch (TaskListValidationException ex)
            {
                throw new TaskListValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<string> Create(TaskListDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                    throw new TaskListValidationException("O título da lista precisa estar preenchido.");

                if (await _context.TaskLists.AnyAsync(x => x.Name.ToLower().Equals(dto.Name.ToLower())))
                    throw new TaskListValidationException("Uma lista com esse título já existe.");

                var user = await GetUser();

                TaskList task = new TaskList
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    User = user,
                    UserId = user.Id
                };

                await _context.TaskLists.AddAsync(task);
                await _context.SaveChangesAsync();

                return "Lista criada com sucesso!";
            }
            catch (TaskListValidationException ex)
            {
                throw new TaskListValidationException(ex.Message);
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
                    throw new TaskListValidationException("ID informado não é válido.");

                var task = await _context.TaskLists.FindAsync(id);

                if (task == null)
                    throw new TaskListValidationException("Lista não encontrada.");

                if (task.TaskItems.Any())
                    _context.TaskItems.RemoveRange(task.TaskItems);

                _context.TaskLists.Remove(task);
                await _context.SaveChangesAsync();

                return "Lista apagada com sucesso!";
            }
            catch (TaskListValidationException ex)
            {
                throw new TaskListValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Update(int id, TaskListDto dto)
        {
            try
            {
                if (id < 0)
                    throw new TaskListValidationException("ID informado não é válido.");

                if (string.IsNullOrEmpty(dto.Name))
                    throw new TaskListValidationException("O título da lista precisa estar preenchido.");

                if (await _context.TaskLists.AnyAsync(x => x.Id != id && x.Name.ToLower().Equals(dto.Name.ToLower())))
                    throw new TaskListValidationException("Uma lista com esse título já existe.");

                var task = await _context.TaskLists.FindAsync(id);

                task.Name = dto.Name;
                task.Description = dto.Description;
                task.UpdateAt = DateTime.UtcNow;

                _context.TaskLists.Update(task);
                await _context.SaveChangesAsync();

                return "Lista atualizada com sucesso!";
            }
            catch (TaskListValidationException ex)
            {
                throw new TaskListValidationException(ex.Message);
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
