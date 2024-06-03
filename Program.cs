using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MyBoardsApp.Controllers;
using MyBoardsApp.DatabaseContext;
using MyBoardsApp.DataGenerator;
using MyBoardsApp.Entities;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyBoardsContext>
                (options => options
	                //.UseLazyLoadingProxies()
	                .UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnectionString"))
                );

builder.Services.Configure<JsonOptions>(o =>
{
	o.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<WorkItemsController>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//	Migrate the database if there are pending migrations
using IServiceScope scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MyBoardsContext>();
var pendingMigrations = context.Database.GetPendingMigrations();
if (pendingMigrations.Any()) context.Database.Migrate();
//	End of migration

//	DataGenerator.Seed(context);

app.Run();