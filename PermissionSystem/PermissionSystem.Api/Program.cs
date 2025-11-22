using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Application.Data;
using PermissionSystem.Application.DependencyInjections;
using PermissionSystem.Application.Services;

Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

var connectionString = ConnectionStringBuilder.BuildMySqlConnectionString();

builder.Services.AddDbContext<PermissionSystemContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();

builder.Services.AddScoped<PermissionService>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<PermissionGroupService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
