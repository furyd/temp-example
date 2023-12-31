using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Example.Api.Services.Implementation;
using Example.Domain.Queries.Registration;
using Example.Domain.Repositories.Registration;
using Example.Domain.Settings.Registration;
using Example.Domain.Shared.Metrics.Registration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddLogging();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});
builder.Services.AddApplicationInsightsTelemetry();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterQueries();
builder.Services.RegisterRepositories();
builder.Services.RegisterSettings(builder.Configuration);
builder.Services.RegisterMetrics();
builder.Services.AddSingleton<ResultService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }