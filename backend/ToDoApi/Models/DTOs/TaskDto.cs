﻿namespace ToDoApi.Models.DTOs;

public record TaskDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
