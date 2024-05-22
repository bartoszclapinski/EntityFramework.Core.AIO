﻿namespace MyBoardsApp.Entities;

public class WorkItemTag
{
    public int WorkItemId { get; set; }
    public virtual WorkItem WorkItem { get; set; }
    
    public int TagId { get; set; }
    public virtual Tag Tag { get; set; }

    public DateTime PublicationDate { get; set; }
}