using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure(builder.Configuration);

var app = builder.Build();

app.Services.ExecuteSeedData();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
}

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:5001", "http://localhost:4200" )
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.MapGet("/api/Health", () => "Ok");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();