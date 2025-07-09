namespace ToDoApi.Models.DTOs;

public record UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
