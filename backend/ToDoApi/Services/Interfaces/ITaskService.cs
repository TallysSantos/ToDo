using ToDoApi.Models.DTOs;

namespace ToDoApi.Services.Interfaces
{
    public interface ITaskService
    {
        Task<string> Create(TaskDto dto);
        Task<string> Delete(int id);
        Task<string> Update(int id, TaskDto dto);
        Task<List<TaskResponseDto>> GetAllTasks(int userId);
    }
}
