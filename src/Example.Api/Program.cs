using System.Text.Json.Serialization;
using Example.Api.Services.Implementation;
using Example.Domain.Queries.Registration;
using Example.Domain.Repositories.Registration;
using Example.Domain.Settings.Registration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterQueries();
builder.Services.RegisterRepositories();
builder.Services.RegisterSettings(builder.Configuration);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
