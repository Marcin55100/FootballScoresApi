using FootballScoresApi.Api;
using FootballScoresApi.Helpers;
using FootballScoresApi.Logger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Logger
LoggerCreator.CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddScoped<IScoresApiProvider, ScoresApiProvider>();
builder.Services.AddScoped<IHttpApiProvider, HttpApiProvider>();
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options =>
options.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader());

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
