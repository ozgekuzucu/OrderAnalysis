using Microsoft.Extensions.Options;
using OrderAnalysis.API.Middlewares;
using OrderAnalysis.Application.Interfaces;
using OrderAnalysis.Application.Mappings;
using OrderAnalysis.Application.Services;
using OrderAnalysis.Infrastructure.Persistence;
using OrderAnalysis.Infrastructure.Repositories;
using OrderAnalysis.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped<MongoDbContext>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

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
