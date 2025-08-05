using BenefitsService.Application;
using BenefitsService.Application.Interfaces;
using BenefitsService.Application.Services;
using BenefitsService.Domain.Interfaces;
using BenefitsService.Infrastructure;
using BenefitsService.Infrastructure.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(MappingConfig.GetMappings());
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeAggregateRepository, EmployeeAggregateRepository>();
builder.Services.AddScoped<IDataRepository, EFDataRepository>();
builder.Services.AddDbContext<BenefitsServiceContext>(options => options
    .UseSqlite(builder.Configuration.GetConnectionString("BenefitsDatabase")));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
