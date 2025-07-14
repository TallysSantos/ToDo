using ToDoApi.Models.DTOs;

namespace ToDoApi.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<string> Create(int id, TaskItemDto dto);
    }
}
