namespace ToDoApi.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public int TaskListId { get; set; }
    public TaskList TaskList { get; set; }
}
