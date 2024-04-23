namespace MyBoardsApp.Entities.WorkItemTypes;

public class Epic : WorkItem
{
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }	
}