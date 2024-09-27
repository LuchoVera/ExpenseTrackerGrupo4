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
using ExpenseTrackerGrupo4.src.Utils;
using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddHangfire(configuration => 
    configuration.UsePostgreSqlStorage("Host=localhost;Port=5432;Database=mydatabase;Username=root;Password=group4321"));


builder.Services.AddHangfireServer();

builder.Services.AddJwtAuthentication();
builder.Services.AddSwaggerWithJwt();
builder.Services.AddControllers();
builder.Services.AddScoped<CommandInvoker>();
builder.Services.AddScoped<ITokenValidatorService, TokenValidatorService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IBudgetNotificationLogRepository, BudgetNotificationLogRepository>();
builder.Services.AddScoped<IGoalNotificationLogRepository, GoalNotificationLogRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddAutoMapper(typeof(ExpenseTrackerProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));

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

app.UseHangfireDashboard();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
