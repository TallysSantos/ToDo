using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Entities;
using ToDoApi.Exceptions;
using ToDoApi.Models.DTOs;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ApplicationDbContext _context;

        public TaskItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Create(int id, TaskItemDto dto)
        {
            try
            {
                if (!await _context.TaskLists.AnyAsync(x => x.Id == id))
                    throw new TaskItemValidationException("List not found.");

                if (dto == null || string.IsNullOrEmpty(dto.Title))
                    throw new TaskItemValidationException("O título do item precisa ser preenchido.");

                if (await _context.TaskItems.AnyAsync(x => x.Title.ToLower().Equals(dto.Title.ToLower())))
                    throw new TaskItemValidationException("Um item já existe com esse título.");

                TaskItem taskItem = new TaskItem
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    DueDate = dto.DueDate,
                    IsCompleted = dto.IsCompleted,
                    TaskListId = id,
                };

                await _context.AddAsync(taskItem);
                await _context.SaveChangesAsync();

                return "Item criado com sucesso!";
            }
            catch (TaskItemValidationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}
