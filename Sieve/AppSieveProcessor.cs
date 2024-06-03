﻿using Microsoft.Extensions.Options;
using MyBoardsApp.Entities.WorkItemTypes;
using Sieve.Models;
using Sieve.Services;

namespace MyBoardsApp.Sieve;

public class AppSieveProcessor : SieveProcessor
{

	public AppSieveProcessor(IOptions<SieveOptions> options) : base(options)
	{
	}

	protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
	{
		mapper.Property<Epic>(e => e.Priority).CanSort().CanFilter();
		mapper.Property<Epic>(e => e.Area).CanSort().CanFilter();
		mapper.Property<Epic>(e => e.StartDate).CanSort().CanFilter();
		mapper.Property<Epic>(e => e.Author.FullName).CanSort().CanFilter().HasName("authorFullName");

		return mapper;
	}
}