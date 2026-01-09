

using NogosServisi.Data;
using NogosServisi.Seed;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();

//messaging
builder.Services.AddSignalR();

var app = builder.Build();

app.UseStaticFiles();

//Default database seeding

/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataContext context = services.GetRequiredService<DataContext>();
    //Log.Information("Starting database seeding...");

    await DefaultSeeds.SeedAsync(context);

    await context.DisposeAsync();
}
*/
//messaging

app.MapHub<ScoreHub>("/scoreHub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();


app.Run();

