using Microsoft.AspNetCore.Mvc;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.Entities;

namespace MyBoardsApp.Controllers;

[ApiController]
[Route("api/tags")]
public class TagsController
{
	private readonly MyBoardsContext _context;

	public TagsController(MyBoardsContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	[HttpPut]
	public async Task<Tag> CreateNewTag()
	{
		var tag = new Tag
		{
			Value = "EF"
		};

		_context.Tags.Add(tag);
		await _context.SaveChangesAsync();

		return tag;
	}

	[HttpPut]
	public async Task<IEnumerable<Tag>> CreateMultipleTags()
	{
		Tag mvc = new Tag { Value = "MVC" };
		Tag asp = new Tag { Value = "ASP" };
		
		var tags = new List<Tag> { mvc, asp };

		await _context.Tags.AddRangeAsync(tags);
		await _context.SaveChangesAsync();

		return tags;
	}
}