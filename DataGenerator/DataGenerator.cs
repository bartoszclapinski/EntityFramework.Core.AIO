using Bogus;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.Entities;

namespace MyBoardsApp.DataGenerator;

public class DataGenerator
{
	public static void Seed(MyBoardsContext context)
	{
		//	Set the seed for the random number generator
		//	so that the generated data is always the same
		//	Randomizer.Seed = new Random(911);
		
		//	Define the locale for the Faker instance
		const string locale = "pl";
		
		// Create a new Faker instance for generating Address objects
		var addressGenerator = new Faker<Address>(locale)
			//.StrictMode(true)	//	Enable strict mode to throw exceptions when a rule cannot be generated
			.RuleFor(a => a.City, f => f.Address.City())
			.RuleFor(a => a.Country, f => f.Address.Country())
			.RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
			.RuleFor(a => a.Street, f => f.Address.StreetName());
		
		//	Create a new Faker instance for generating User objects
		var userGenerator = new Faker<User>(locale)
			.RuleFor(u => u.Email, f => f.Person.Email)
			.RuleFor(u => u.FullName, f => f.Person.FullName)
			.RuleFor(u => u.Address, f => addressGenerator.Generate());

		//	Generate 10 users
		var users = userGenerator.Generate(10);
		
		context.AddRangeAsync(users);
		//context.SaveChangesAsync();
	}
}