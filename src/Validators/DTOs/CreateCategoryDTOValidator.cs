using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using FluentValidation;

namespace ExpenseTrackerGrupo4.src.Validators.DTOs;

public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
{
    public CreateCategoryDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

        RuleFor(x => x.ParentId)
            .NotEmpty().When(x => x.ParentId.HasValue).WithMessage("Parent ID must be a valid GUID.");
    }
}
