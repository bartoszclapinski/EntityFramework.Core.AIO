using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Controllers;

[ApiController]
[Route("api/states")]
public class StatesController
{
	private readonly MyBoardsContext _context;

	public StatesController(MyBoardsContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}
	
	[HttpGet]
	public async Task<IEnumerable<WorkItemState>> GetAllStates()
	{
		var states = await _context.WorkItemStates.AsNoTracking().ToListAsync();
		//	For debugging purposes
		var entries = _context.ChangeTracker.Entries();
		
		return states;
	}
	
	[HttpGet("/api/states/sqlraw/")]
	public async Task<IEnumerable<WorkItemState>> ExecuteRawSql()
	{
		var states = await _context.WorkItemStates
			.FromSqlRaw("""
			            SELECT wis.WorkItemStateId, wis.Value FROM WorkItemStates wis
			            JOIN WorkItems wi ON wi.WorkItemStateId = wis.WorkItemStateId
			            GROUP BY wis.WorkItemStateId, wis.Value
			            HAVING COUNT(*) > 85
			            """)
			.AsNoTracking()
			.ToListAsync();

		return states;
	}
	
	[HttpGet("/api/states/sqlinterpolated/")]
	public async Task<IEnumerable<WorkItemState>> ExecuteSqlInterpolated()
	{
		var count = 85;
		var states = await _context.WorkItemStates
			.FromSqlInterpolated($"""
			            SELECT wis.WorkItemStateId, wis.Value FROM WorkItemStates wis
			            JOIN WorkItems wi ON wi.WorkItemStateId = wis.WorkItemStateId
			            GROUP BY wis.WorkItemStateId, wis.Value
			            HAVING COUNT(*) > {count}
			            """)
			.AsNoTracking()
			.ToListAsync();

		return states;
	}
	
}