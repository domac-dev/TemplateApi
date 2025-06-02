using Api;
using Infrastructure;
using Infrastructure.Seed;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.BuildProject();

var app = builder.Build();
app.BuildApp();
app.UseHttpsRedirection();

/*Uncomment this line if you want to automatically create and seed database*/
//SeedDb(app);

app.Run();

static void SeedDb(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<Database>();
        context.Database.EnsureCreated();
        DatabaseSeeder.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
}

public partial class Program { }