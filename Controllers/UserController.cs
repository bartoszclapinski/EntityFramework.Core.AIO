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

	[HttpPut]
	public async Task<User> CreateNewUser()
	{
		var address = new Address
		{
			AddressId = new Guid(),
			City = "Warsaw",
			Country = "Poland",
			Street = "Długa 1"
		};

		var user = new User
		{
			Email = "user@email.com",
			FullName = "John Doe",
			Address = address
		};
		
		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return user;
	}

	[HttpGet("{userId:Guid}")]
	public async Task<User> GetUserByIdWithComments(Guid userId)
	{
		User result = await _context.Users
			.Include(u => u.Comments).ThenInclude(c => c.WorkItem)
			.Include(u => u.Address)
			.FirstOrDefaultAsync(u => u.UserId == userId);

		return result;
	} 
	
}