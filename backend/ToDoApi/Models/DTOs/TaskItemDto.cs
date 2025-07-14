using System.ComponentModel;

namespace ToDoApi.Models.DTOs;

public record TaskItemDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    [DefaultValue(null)]
    public DateTime? DueDate { get; set; }
    [DefaultValue(false)]
    public bool IsCompleted { get; set; } = false;

}
