namespace MyBoardsApp.Entities;

public class WorkItemTag
{
    public int WorkItemId { get; set; }
    public WorkItem WorkItem { get; set; }
    
    public int TagId { get; set; }
    public Tag Tag { get; set; }

    public DateTime PublicationDate { get; set; }
}