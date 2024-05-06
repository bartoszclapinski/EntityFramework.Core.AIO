using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.Entities;

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
	
	
}