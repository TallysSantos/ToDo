namespace ToDoApi.Entities;

public class TaskList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateAt { get; set; } = null;
    public int UserId { get; set; }
    public ApplicationUser User { get; set; }

    public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}