using DinarInvestments.Application.Services.Implementations;
using DinarInvestments.Application.Services.Interfaces;
using DinarInvestments.Domain.DomainServices;
using DinarInvestments.Domain.Repositories;
using DinarInvestments.Infrastructure;
using DinarInvestments.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<InvestorDbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionString"]));

// Configure Services
builder.Services.TryAddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.TryAddTransient<IInvestorService, InvestorService>();
builder.Services.TryAddTransient<IInvestmentService, InvestmentService>();
builder.Services.TryAddTransient<IInvestorRepository, InvestorRepository>();
builder.Services.TryAddTransient<IInvestmentOpportunityDomainService, InvestmentOpportunityDomainService>();


// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dinar Investments API", Version = "v1" });
});

// Configure Health Checks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration["ConnectionString"],
        name: "Postgres",
        tags: ["InvestorService", "service"]);


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DinarInvestments API v1"); });
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();