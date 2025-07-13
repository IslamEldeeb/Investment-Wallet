using DinarInvestments.API.Helpers;
using DinarInvestments.API.Utilities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<ConfigSettings>(builder.Configuration.GetSection("ConfigSettings"));

// Configure Services
builder.Services.RegisterServicesByConvention();

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