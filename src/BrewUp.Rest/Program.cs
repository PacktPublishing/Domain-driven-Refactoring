using BrewUp.Rest.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterModules();

var app = builder.Build();

app.ConfigureModules();

app.Run();