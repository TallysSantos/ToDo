namespace ToDoApi.Models.DTOs;

public record TaskListResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<TaskItemResponseDto> Items { get; set; }
}