﻿namespace MyBoardsApp.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public Address Address { get; set; }
    public List<WorkItem> WorkItems { get; set; } = new();
}