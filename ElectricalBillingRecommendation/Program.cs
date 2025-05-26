using ElectricalBillingRecommendation.Data;
using ElectricalBillingRecommendation.Services.Interfaces;
using ElectricalBillingRecommendation.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITaxGroupService, TaxGroupService>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // log u konzolu
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // log u fajl po danu
    .CreateLogger();
builder.Host.UseSerilog(); // koristi Serilog kao logger

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
