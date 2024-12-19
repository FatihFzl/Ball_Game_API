using Ball_Game_API.Ball;
using Ball_Game_API.Data;
using Ball_Game_API.GameCharacters;
using Ball_Game_API.Hubs;
using Ball_Game_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));
builder.Services.AddSignalR();
builder.Services.AddScoped<IScoreHistoryService, ScoreHistoryService>();
builder.Services.AddSingleton<GameCharactersManager>();
builder.Services.AddSingleton<GameRunnerBackgroundJob>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<GameRunnerBackgroundJob>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .WithOrigins("http://localhost:5173")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapHub<GameHub>("/gameHub");

app.Run();



