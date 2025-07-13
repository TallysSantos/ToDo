using ToDoApi.Models.DTOs;

namespace ToDoApi.Services.Interfaces
{
    public interface ITaskListService
    {
        Task<string> Create(TaskListDto dto);
        Task<string> Delete(int id);
        Task<string> Update(int id, TaskListDto dto);
        Task<List<TaskListResponseDto>> GetAllTaskLists(int userId);
    }
}
