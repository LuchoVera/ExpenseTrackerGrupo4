using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Utils;
using Microsoft.Extensions.Caching.Distributed;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class GoalService (
    IGoalRepository goalRepository, CommandInvoker commandInvoker,
    IEmailService emailService, IDistributedCache cache,
    IGoalNotificationLogRepository notificationLogRepository,
    IUserRepository userRepository
) : BaseService(commandInvoker), IGoalService
{
    private readonly IGoalRepository _goalRepository = goalRepository;
    private readonly IEmailService _emailService = emailService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IDistributedCache _cache = cache;

    private readonly IGoalNotificationLogRepository _notificationLogRepository = notificationLogRepository;

    public async Task AddAsync(Goal goal)
    {
        var command = new AddGoalCommand(_goalRepository, goal);
        await CommandInvoker.Execute(command);
    }

    public async Task DeleteAsync(Guid id, Guid userId)
    {
        var command = new DeleteGoalCommand(_goalRepository, id, userId);
        await CommandInvoker.Execute(command);
    }

    public async Task<Goal?> GetByIdAsync(Guid id, Guid userId)
    {
        var command = new GetGoalByIdCommand(_goalRepository, id, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task<List<GoalWithDetails>> GetGoalsWithDetailsAsync(Guid userId)
    {
        var command = new GetUserGoalsCommand(_goalRepository, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task UpdateAsync(Goal goal, Guid userId)
    {
        var command = new UpdateGoalCommand(_goalRepository, goal, userId);
        await CommandInvoker.Execute(command);

        await ResetGoalNotificationsAsync(goal, userId);

        await CheckGoalNotificationsAsync(userId);
    }
    private async Task ResetGoalNotificationsAsync(Goal goal, Guid userId)
    {
        var updatedGoal = await _goalRepository.GetByIdAsync(goal.Id);
        if (updatedGoal == null) return;

        var log = await _notificationLogRepository.GetByGoalIdAsync(goal.Id, userId);
        if (log == null)
        {
            log = new GoalNotificationLog
            {
                UserId = userId,
                GoalId = goal.Id,
                Notified50 = false,
                Notified100 = false
            };
        }

        if (updatedGoal.CurrentAmount < updatedGoal.GoalAmount * 0.5m)
        {
            log.Notified50 = false;
        }

        if (updatedGoal.CurrentAmount < updatedGoal.GoalAmount)
        {
            log.Notified100 = false;
        }

        await _notificationLogRepository.InsertOrUpdateAsync(log);
    }

    public async Task CheckGoalNotificationsAsync(Guid userId)
    {
        var goals = await _goalRepository.GetGoalsWithDetailsAsync(userId);

        foreach (var goal in goals)
        {
            var log = await _notificationLogRepository.GetByGoalIdAsync(goal.GoalId, userId);

            if (log == null)
            {
                log = new GoalNotificationLog
                {
                    UserId = userId,
                    GoalId = goal.GoalId,
                    Notified50 = false,
                    Notified100 = false
                };
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) continue;

            if (goal.CurrentAmount >= goal.GoalAmount * 0.5m && !log.Notified50)
            {
                await _emailService.SendEmailAsync(user.Email, "Savings Goal Milestone", "You have reached 50% of your savings goal.");
                log.Notified50 = true;
            }

            if (goal.CurrentAmount >= goal.GoalAmount && !log.Notified100)
            {
                await _emailService.SendEmailAsync(user.Email, "Savings Goal Milestone", "Congratulations! You have reached your savings goal.");
                log.Notified100 = true;
            }

            await _notificationLogRepository.InsertOrUpdateAsync(log);
        }
    }
}
