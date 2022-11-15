using FluentValidation;

namespace AttributeService.Application.Features.Attributes.Commands.SaveAttribute
{
    public class SaveAttributeCommandValidator : AbstractValidator<SaveAttributeCommand>
    {
        public SaveAttributeCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{Name} is required.")
               .NotNull()
               .MaximumLength(250).WithMessage("{Name} must not be over 250 characters.");
        }
    }
}
