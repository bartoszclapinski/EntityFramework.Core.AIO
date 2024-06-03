namespace MyBoardsApp.DTO;

public class EpicDto
{
	public int WorkItemId { get; set; }
	public int Priority { get; set; }
	public string Area { get; set; }
	public DateTime? StartDate { get; set; }
	public string AuthorFullName { get; set; }
}