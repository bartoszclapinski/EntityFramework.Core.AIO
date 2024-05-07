using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.Entities;
using MyBoardsApp.Entities.WorkItemTypes;

namespace MyBoardsApp.Controllers;

[ApiController]
[Route("api/work-items")]
public class WorkItemsController
{
	private readonly MyBoardsContext _context;

	public WorkItemsController(MyBoardsContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IEnumerable<WorkItem>> GetAll()
	{
		var workItems = await _context.WorkItems.ToListAsync();
		return workItems;
	}

	[HttpGet("{stateId}")]
	public async Task<IEnumerable<WorkItem>> GetByStateId([FromRoute] int stateId)
	{
		var workItems = await _context.WorkItems.Where(w => w.WorkItemStateId == stateId).ToListAsync();
		return workItems;
	}

	[HttpGet("count")]
	public async Task<IEnumerable<object>> GetStateCount()
	{
		return await _context.WorkItems
			.GroupBy(w => w.WorkItemStateId)
			.Select(g => new { WorkItemStateId = g.Key, Count = g.Count()})
			.ToListAsync();
	}

	[HttpGet("epic/{stateId:int}")]
	public async Task<IEnumerable<WorkItem>> GetEpicsByStateIdOrderedByPriority([FromRoute] int stateId = 4)
	{
		var result = await _context.WorkItems
			.Where(w => w.WorkItemStateId == stateId)
			.OrderBy(w => w.Priority)
			.ToListAsync();

		return result;
	}
	
	[HttpGet("{epicId:int}")]
	public async Task<Epic> GetEpic([FromRoute] int epicId)
	{
		return await _context.Epics.FirstOrDefaultAsync(e => e.WorkItemId == epicId);
	}

	[HttpPost("{epicId:int}")]
	public async Task<WorkItem> UpdateEpic([FromRoute] int epicId)
	{
		Epic epic = await _context.Epics.FirstOrDefaultAsync(e => e.WorkItemId == epicId);
		epic.Area = epic.Area + " Updated";
		epic.StartDate = DateTime.Now;
		await _context.SaveChangesAsync();

		return epic;
	}
	
	[HttpPost("{epicId:int},{stateId:int}")]
	public async Task<WorkItem> UpdateEpicState([FromRoute] int epicId, [FromRoute] int stateId)
	{
		Epic epic = await _context.Epics.FirstOrDefaultAsync(e => e.WorkItemId == epicId);
		WorkItemState state = await _context.WorkItemStates.FirstOrDefaultAsync(s => s.WorkItemStateId == stateId);
		epic.WorkItemState = state;
		await _context.SaveChangesAsync();

		return epic;
	}
	
	
	
	
    
}