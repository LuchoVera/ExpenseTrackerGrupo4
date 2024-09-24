using ExpenseTrackerGrupo4.src.Domain.Contexts;
using System.Data;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Infrastructure.Repositories;
using Npgsql;
using ExpenseTrackerGrupo4.src.Presentation.Profiles;
using DotNetEnv;
using ExpenseTrackerGrupo4.Configurations;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Aplication.Services;
using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddJwtAuthentication();
builder.Services.AddSwaggerWithJwt();
builder.Services.AddControllers();
builder.Services.AddScoped<CommandInvoker>();
builder.Services.AddScoped<ITokenValidatorService, TokenValidatorService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IIncomeService, IncomeService>();

builder.Services.AddAutoMapper(typeof(ExpenseTrackerProfile));

builder.Services.AddTransient<IDbConnection>(sp => 
    new NpgsqlConnection("Host=localhost;Port=5432;Database=mydatabase;Username=root;Password=group4321"));
    
builder.Services.AddScoped<BaseContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Expense Tracker API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
