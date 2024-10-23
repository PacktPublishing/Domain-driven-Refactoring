using BrewUp.DomainModel.Services;
using BrewUp.Infrastructure.MongoDb;
using BrewUp.ReadModel;
using BrewUp.ReadModel.Sales.Queries;
using BrewUp.ReadModel.Sales.Services;
using BrewUp.ReadModel.Warehouses.Queries;
using BrewUp.ReadModel.Warehouses.Services;
using BrewUp.Rest.Services;
using BrewUp.Rest.Validators.Warehouses;
using BrewUp.Shared.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
builder.Services.AddCors(options => { options.AddPolicy("CorsPolicy", corsBuilder => corsBuilder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader()); });
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog(logger);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
	Description = "BrewUp",
	Title = "BrewUp API",
	Version = "v1",
	Contact = new OpenApiContact
	{
		Name = "BrewUp"
	}
}));

builder.Services.AddMongoDb(builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!);

builder.Services.AddKeyedScoped<IRepository, SaleRepository>("sale");
builder.Services.AddKeyedScoped<IRepository, WarehouseRepository>("warehouse");

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<ISalesOrderService, BrewUp.DomainModel.Services.SalesOrderService>();
builder.Services.AddScoped<ISalesQueryService, SalesQueryService>();
builder.Services.AddScoped<IQueries<SalesOrder>, SalesOrderQueries>();

builder.Services.AddValidatorsFromAssemblyContaining<SetAvailabilityValidator>();
builder.Services.AddSingleton<ValidationHandler>();
builder.Services.AddScoped<IWarehouseService, BrewUp.DomainModel.Services.WarehouseService>();
builder.Services.AddScoped<IAvailabilityQueryService, AvailabilityQueryService>();
builder.Services.AddScoped<IQueries<Availability>, AvailabilityQueries>();

var app = builder.Build();

app.UseCors("CorsPolicy");

//Sales
var salesGroup = app.MapGroup("/v1/sales/").WithTags("Sales");
salesGroup.MapPost("/", BrewUp.Rest.Services.SalesOrderService.HandleCreateSalesOrder)
	.Produces(StatusCodes.Status400BadRequest)
	.Produces(StatusCodes.Status201Created)
	.WithName("CreateSalesOrder");

salesGroup.MapGet("/", BrewUp.Rest.Services.SalesOrderService.HandleGetOrders)
	.Produces(StatusCodes.Status404NotFound)
	.Produces(StatusCodes.Status200OK)
	.WithName("GetSalesOrders");

//Warehouses
var warehousesGroup = app.MapGroup("/v1/warehouses/").WithTags("Warehouses");
warehousesGroup.MapPost("/availabilities", WarehousesService.HandleSetAvailabilities)
	.Produces(StatusCodes.Status400BadRequest)
	.Produces(StatusCodes.Status200OK)
	.WithName("SetAvailabilities");

// Configure the HTTP request pipeline.
app.UseSwagger(s =>
{
	s.RouteTemplate = "documentation/{documentName}/documentation.json";
});
app.UseSwaggerUI(s =>
{
	s.SwaggerEndpoint("/documentation/v1/documentation.json", "BrewUp");
	s.RoutePrefix = "documentation";
});

await app.RunAsync();