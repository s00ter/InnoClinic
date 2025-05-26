using FluentValidation;
using InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;

namespace InnoClinic.ServiceApi.BusinessLogic.Validators.ServiceCategoryValidators;

public class UpdateServiceCategoryValidator : AbstractValidator<UpdateServiceCategoryRequest>
{
    public UpdateServiceCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.TimeSlotSize)
            .NotEmpty().WithMessage("TimeSlotSize is required");
    }
}