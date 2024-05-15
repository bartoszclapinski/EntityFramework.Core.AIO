using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Controllers;

[ApiController]
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
	
}