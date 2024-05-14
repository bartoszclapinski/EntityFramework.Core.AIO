using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardsApp.DatabaseContext;

namespace MyBoardsApp.Controllers;

[ApiController]
public class WorkItemTagsController
{
	private readonly MyBoardsContext _context;

	public WorkItemTagsController(MyBoardsContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}
	
	[HttpDelete("{workItemId:int}")]
	public async Task DeleteTagsForWorkItem([FromRoute] int workItemId)
	{
		var tags = await _context.WorkItemTag.Where(wt => wt.WorkItemId == workItemId).ToListAsync();
		_context.WorkItemTag.RemoveRange(tags);
		await _context.SaveChangesAsync();
	}
}