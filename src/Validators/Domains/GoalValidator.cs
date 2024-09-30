using ExpenseTrackerGrupo4.src.Domain.Entities;
using FluentValidation;

namespace ExpenseTrackerGrupo4.src.Validators;

public class GoalValidator : AbstractValidator<Goal>
{
    public GoalValidator()
    {
        RuleFor(g => g.UserId).NotEmpty().WithMessage("User ID is required.");

        RuleFor(g => g.GoalAmount)
            .GreaterThan(0)
            .WithMessage("Goal amount must be greater than zero.");

        RuleFor(g => g.CurrentAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Current amount must be at least zero.");

        RuleFor(g => g.Deadline)
            .GreaterThan(DateTime.Now)
            .WithMessage("Deadline must be a future date.");
    }
}