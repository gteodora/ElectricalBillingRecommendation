using ElectricalBillingRecommendation.Data;
using ElectricalBillingRecommendation.Services.Interfaces;
using ElectricalBillingRecommendation.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ElectricalBillingRecommendation.Repositories.Interfaces;
using ElectricalBillingRecommendation.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // log u konzolu
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // log u fajl po danu
    .CreateLogger();
builder.Host.UseSerilog(); // koristi Serilog kao logger

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ElectricalBillingRecommendation.Services.Interfaces.ITaxGroupService, TaxGroupService>();
builder.Services.AddScoped<ElectricalBillingRecommendation.Services.Interfaces.IPlanService, PlanService>();
builder.Services.AddScoped<IPricingTierService, PricingTierService>();
builder.Services.AddScoped<ElectricalBillingRecommendation.Repositories.Interfaces.ITaxGroupService, TaxGroupRepository>();  //da li je scoped ili?
builder.Services.AddScoped<ElectricalBillingRecommendation.Repositories.Interfaces.IPlanService, PlanRepository>();
builder.Services.AddScoped<IPricingTierRepository, PricingTierRepository>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();

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
