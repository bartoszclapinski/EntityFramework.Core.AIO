using LinqToDB;
using LinqToDB.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Controllers;


[ApiController]
[Route("api/comments")]
public class CommentsController
{
	private readonly MyBoardsContext _context;

	public CommentsController(MyBoardsContext context)
	{
		_context = context;
	}
	
	[HttpGet]
	public async Task<IEnumerable<Comment>> GetAll()
	{
		var comments = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Comments);
		return comments;
	}

	[HttpGet("{year:int},{month:int},{day:int}")]
	public async Task<IEnumerable<Comment>> GetCommentsNewerThan(int year, int month, int day)
	{
		var comments = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Comments
				.Where(c => c.CreatedAt > new DateTime(year, month, day)));

		return comments;
	}

	[HttpGet("{count:int}")]
	public async Task<IEnumerable<Comment>> GetTopCommentsByDate(int count = 5)
	{
		var comments = await EntityFrameworkQueryableExtensions.ToListAsync(_context.Comments.OrderBy(c => c.CreatedAt).Take(count));
		return comments;
	}

	[HttpGet("count-by-user")]
	public async Task<User> GetUserWithMostComments()
	{
		var authorId = _context.Comments
			.GroupBy(c => c.AuthorId)
			.Select(g => new { AuthorId = g.Key, Count = g.Count() })
			.OrderByDescending(s => s.Count);
		
		return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_context.Users, u => u.UserId == authorId.First().AuthorId);
	}

	[HttpPut("update-all-comments")]
	public async Task UpdateAllComments()
	{
		await _context.Comments
			.Where(c => c.CreatedAt > new DateTime(2022, 6, 27))
			.ToLinqToDB()
			.Set(c => c.Message, c => c.Message + " (Updated)")
			.UpdateAsync();
	}
}