using Domain;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IJsonManager>(j => new JsonManager(entitiesFolderPath: "./Entities"));
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();
