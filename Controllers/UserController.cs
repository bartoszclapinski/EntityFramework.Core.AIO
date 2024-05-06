using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Controllers;

[ApiController]
[Route("api/data")]
public class UserController
{
	private readonly MyBoardsContext _context;

	public UserController(MyBoardsContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IEnumerable<User>> GetAllUsers()
	{
		var users = await _context.Users.ToListAsync();
		return users;
	}
	
}