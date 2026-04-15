using Application.Servicios;
using GestionDeTareas.API.Extensiones;
using GestionDeTareas.API.Middlewares;
using GestionDeTareas.API.Servicios;
using Infrastructure.Servicios;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentation(builder.Configuration)
                .AddInfrastructure(builder.Configuration)
                .AddAplication();

var environment = builder.Environment.EnvironmentName;
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

Console.WriteLine($"Entorno actual: {builder.Environment.EnvironmentName}");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();


app.UseCors("web");

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

// Endpoint para el Load Balancer de AWS
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(x => new
            {
                component = x.Key,
                status = x.Value.Status.ToString(),
                description = x.Value.Description
            }),
            duration = report.TotalDuration
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
});

app.Run();
