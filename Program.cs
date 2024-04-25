using Microsoft.EntityFrameworkCore;
using MyBoardsApp.DatabaseContext;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyBoardsContext>
                (options => options.UseSqlServer(
                                builder.Configuration.GetConnectionString("MyBoardsConnectionString"))
                );

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

app.Run();