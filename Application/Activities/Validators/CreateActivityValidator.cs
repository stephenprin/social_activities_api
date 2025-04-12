using Application.Activities.Command;
using FluentValidation;

namespace Application.Activities.Validators;

public class CreateActivityValidator: AbstractValidator<CreateActivity.Command>
{
    public CreateActivityValidator()
    {
        RuleFor(x => x.createActivityDto.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.createActivityDto.Date).NotEmpty().WithMessage("Date is required");
        RuleFor(x => x.createActivityDto.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.createActivityDto.Category).NotEmpty().WithMessage("Category is required");
    }
}
