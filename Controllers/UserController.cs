using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.DTO;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Controllers;

[ApiController]
[Route("api/users")]
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

	[HttpGet("{userId}")]
	public async Task<User> GetUserByIdWithComments(string userId)
	{
		User result = await _context.Users
			.Include(u => u.Comments).ThenInclude(c => c.WorkItem)
			.Include(u => u.Address)
			.FirstOrDefaultAsync(u => u.UserId == new Guid(userId));

		return result;
	} 
	
	[HttpDelete("{userId}")]
	public async Task DeleteUser(string userId)
	{
		User user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == new Guid(userId));
		var comments = await _context.Comments.Where(c => c.AuthorId == user.UserId).ToListAsync();
		
		//	Remove all comments by the user
		_context.Comments.RemoveRange(comments);
		await _context.SaveChangesAsync();
		
		//	Remove the user
		_context.Users.Remove(user);
		await _context.SaveChangesAsync();
	}

	[HttpDelete("{userId}/cascade")]
	public async Task DeleteUserWithComments(string userId)
	{
		User user = _context.Users.Include(u => u.Comments).FirstOrDefault(u => u.UserId == new Guid(userId));
		if (user != null) _context.Users.Remove(user);
		await _context.SaveChangesAsync();
	}
	
	[HttpPost("{userId}")]
	public async Task<User> UpdateUserEmail(string userId)
	{
		User user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == new Guid(userId));
		//	For debugging purposes
		var entries1 = _context.ChangeTracker.Entries();
		
		user.Email = user.Email + " Updated";
		//	For debugging purposes
		var entries2 = _context.ChangeTracker.Entries();
		
		await _context.SaveChangesAsync();

		return user;
	}
	
	[HttpGet("lazy-loading/{userId}")]
	public async Task<object> GetUserWithLazyLoading(string userId)
	{
		var withAddress = true;
		
		User user =  await _context.Users.FirstOrDefaultAsync(u => u.UserId == new Guid(userId));

		if (withAddress)
		{
			var result = new {FullName = user.FullName, Address = $"{user.Address.Street}, {user.Address.City}, {user.Address.Country}"};
			return result;
		}

		return new { FullName = user.FullName, Address = "-" };
	}
	
	[HttpGet("pagination/{filter}/{sortBy}/{page}/{pageSize}")]
	public async Task<PagedResult<User>> GetUsersWithFilterAndPagination(string filter, string sortBy, int page, int pageSize)
	{
		var sortByDesc = false;
		var query = _context.Users
			.Where(u => filter == null ||
			            u.Email.ToLower().Contains(filter.ToLower()) ||
			            u.FullName.ToLower().Contains(filter.ToLower()));

		var totalCount = query.Count();
		
		if (sortBy != null)
		{
			var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>
			{
				{ nameof(User.Email), user => user.Email },
				{ nameof(User.FullName), user => user.FullName }
			};
			Expression<Func<User, object>> sortByExpression = columnsSelector[sortBy];

			query = sortByDesc 
				? query.OrderByDescending(sortByExpression)
				: query.OrderBy(sortByExpression);
		}
		
		var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
		var pagedResult = new PagedResult<User>(result, totalCount, pageSize, page);

		return pagedResult;
	}

	[HttpGet("from-country/just-fullname/{country}")]
	public async Task<IEnumerable<object>> GetUsersFromCountry(string country)
	{
		var users = await _context.Users
			.Include(u => u.Address)
			.Where(u => u.Address.Country == country)
			.Select(u => u.FullName)
			.ToListAsync();

		return users;
	}

	[HttpGet("from-country/with-comments/{country}")]
	public async Task<IEnumerable<object>> GetUsersFromCountryWithComments(string country)
	{
		var users = await _context.Users
			.Include(u => u.Comments)
			.Include(u => u.Address)
			.Where(u => u.Address.Country == country)
			.Select(u => new { u.FullName, Comments = u.Comments.Select(c => c.Message) })
			.ToListAsync();

		return users;
	}
	
	[HttpGet("get-user-with-comments/{userId}")]
	public async Task<object> GetUserWithCommentsByUserId(string userId)
	{
		User user = await GetUser(new Guid(userId), o => o.Comments);
		return user;
	}
	
	[HttpGet("get-user-with-comments-and-address/{userId}")]
	public async Task<User> GetUserWithCommentsAndAddressByUserId(string userId)
	{
		User user = await GetUser(new Guid(userId), o => o.Comments, o => o.Address);
		return user;
	}
	
	

	private async Task<User> GetUser (Guid userId, params Expression<Func<User, object>>[] includes)
	{
		var baseQuery = _context.Users.AsQueryable();

		if (includes.Any())
		{
			foreach (var include in includes)
			{
				baseQuery = baseQuery.Include(include);
			}
		}
		
		User user = await baseQuery.FirstOrDefaultAsync(u => u.UserId == userId);
		return user;
	}
}