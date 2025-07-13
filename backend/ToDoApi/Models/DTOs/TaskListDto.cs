namespace ToDoApi.Models.DTOs;

public record TaskListDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
