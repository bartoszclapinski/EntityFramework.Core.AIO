﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyBoardsApp.Entities;

public class WorkItem
{
    public int WorkItemId { get; set; }
    public string Area { get; set; }
    public string IterationPath { get; set; }
    public int Priority { get; set; }
    
    // Epic
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    // Issue
    public decimal Effort { get; set; }
    
    // Task
    public string Activity { get; set; }
    public decimal RemainingWork { get; set; }
    public string Type { get; set; }

    public List<Comment> Comments { get; set; } = new();
    public User Author { get; set; }
    public Guid AuthorId { get; set; }
    public List<Tag> Tags { get; set; }

    
    public int WorkItemStateId { get; set; }
    public WorkItemState WorkItemState { get; set; }
    
    
    

}