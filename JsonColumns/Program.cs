using JsonColumns.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MapContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDb")));

var app = builder.Build();

CreateDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void CreateDb()
{
    using var scope = app.Services.CreateScope();
    using var db = scope.ServiceProvider.GetRequiredService<MapContext>();

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    db.Countries.AddRange(
        new Country
        {
            Name = "UK",
            Shape = new Fillarea
            {
                Colour = "Red",
                Coordinates = new() { new(50.06, -5.73), new(51.15, 1.37), new(58.65, -3.07), new(57.60, -7.66) }
            }
        },
        new Country
        {
            Name = "USA",
            Shape = new Fillarea
            {
                Colour = "Blue",
                Coordinates = new() { new(48.36, -124.64), new(32.58, -117.08), new(25.98, -97.21), new(25.26, -80.52), new(44.81, -67.05) }
            }
        },
        new Country
        {
            Name = "Australia",
            Shape = new Fillarea
            {
                Colour = "Blue",
                Coordinates = new() { new(-34.72, 115.60), new(-22.08, 114.52), new(-13.74, 126.85), new(-10.96, 142.71), new(-26.15, 152.77), new(-38.60, 146.32) }
            }
        },
        new Country
        {
            Name = "India",
            Shape = new Fillarea
            {
                Colour = "Green",
                Coordinates = new() { new(8.09, 77.44), new(23.66, 68.38), new(32.34, 75.61), new(21.94, 88.92) }
            }
        }
    );

    db.SaveChanges();
}
