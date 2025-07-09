using Microsoft.AspNetCore.Identity;

namespace ToDoApi.Entities;

public class ApplicationUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();
}