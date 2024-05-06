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
		var comments = await _context.Comments.ToListAsync();
		return comments;
	}

	[HttpGet("{year:int},{month:int},{day:int}")]
	public async Task<IEnumerable<Comment>> GetCommentsNewerThan(int year, int month, int day)
	{
		var comments = await _context.Comments
			.Where(c => c.CreatedAt > new DateTime(year, month, day))
			.ToListAsync();

		return comments;
	}

	[HttpGet("{count:int}")]
	public async Task<IEnumerable<Comment>> GetTopCommentsByDate(int count = 5)
	{
		var comments = await _context.Comments.OrderBy(c => c.CreatedAt).Take(count).ToListAsync();
		return comments;
	}
}