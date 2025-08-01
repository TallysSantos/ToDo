﻿namespace ToDoApi.Models.DTOs;

public record UserRegisterDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
