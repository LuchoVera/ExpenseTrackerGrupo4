using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Utils;
using Microsoft.Extensions.Caching.Distributed;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class BudgetService (
    IBudgetRepository budgetRepository, CommandInvoker commandInvoker,
    IEmailService emailService, IDistributedCache cache,
    IBudgetNotificationLogRepository notificationLogRepository,
    IUserRepository userRepository
) : BaseService(commandInvoker), IBudgetService
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly IEmailService _emailService = emailService;
    private readonly IDistributedCache _cache = cache;
    private readonly IBudgetNotificationLogRepository _notificationLogRepository = notificationLogRepository;
    private readonly IUserRepository _userRepository = userRepository;


    public async Task AddAsync(Budget budget)
    {
        var command = new AddBudgetCommand(_budgetRepository, budget);
        await CommandInvoker.Execute(command);
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var command = new DeleteBudgetCommand(_budgetRepository, id, userId);
        await CommandInvoker.Execute(command);
    }

    public async Task<Budget?> GetByIdAsync(Guid id, Guid userId)
    {
        var command = new GetBudgetByIdCommand(_budgetRepository, id, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task<List<BudgetWithExpenses>> GetBudgetsAsync(Guid userId)
    {
        var command = new GetUserBudgetsCommand(_budgetRepository, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task UpdateAsync(Budget budget, Guid userId)
    {
        var command = new UpdateBudgetCommand(_budgetRepository, budget, userId);
        await CommandInvoker.Execute(command);
    }


    public async Task CheckBudgetNotificationsAsync(Guid userId)
        {
            var budgets = await _budgetRepository.GetBudgetsAsync(userId);

            foreach (var budget in budgets)
            {
                var currentAmount = budgets
                    .Where(b => b.BudgetId == budget.BudgetId)
                    .Sum(b => b.Amount ?? 0);

                var log = await _notificationLogRepository.GetByBudgetIdAsync(budget.BudgetId, userId);

                if (log == null)
                {
                    log = new BudgetNotificationLog
                    {
                        UserId = userId,
                        BudgetId = budget.BudgetId,
                        Notified80 = false,
                        Notified90 = false,
                        Notified100 = false
                    };
                }

                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null) continue;

                if (currentAmount >= budget.BudgetAmount * 0.8m && !log.Notified80)
                {
                    await _emailService.SendEmailAsync(user.Email, "Budget Alert", "You have reached 80% of your budget.");
                    log.Notified80 = true;
                }
                if (currentAmount >= budget.BudgetAmount * 0.9m && !log.Notified90)
                {
                    await _emailService.SendEmailAsync(user.Email, "Budget Alert", "You have reached 90% of your budget.");
                    log.Notified90 = true;
                }
                if (currentAmount >= budget.BudgetAmount && !log.Notified100)
                {
                    await _emailService.SendEmailAsync(user.Email, "Budget Alert", "You have reached your maximum budget.");
                    log.Notified100 = true;
                }

                await _notificationLogRepository.InsertOrUpdateAsync(log);
            }
        }
    }
