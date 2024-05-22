using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyBoardsApp.Entities;

public abstract class WorkItem
{
    public int WorkItemId { get; set; }
    public string Area { get; set; }
    public string IterationPath { get; set; }
    public int Priority { get; set; }
    public virtual List<Comment> Comments { get; set; } = new();
    public virtual User Author { get; set; }
    public Guid AuthorId { get; set; }
    public virtual List<Tag> Tags { get; set; }
    public int WorkItemStateId { get; set; }
    public virtual WorkItemState WorkItemState { get; set; }
    
    
    

}