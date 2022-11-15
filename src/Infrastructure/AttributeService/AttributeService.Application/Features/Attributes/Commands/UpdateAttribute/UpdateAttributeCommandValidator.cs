using FluentValidation;

namespace AttributeService.Application.Features.Attributes.Commands.UpdateAttribute
{
    public class UpdateAttributeCommandValidator : AbstractValidator<UpdateAttributeCommand>
    {
        public UpdateAttributeCommandValidator()
        {
            RuleFor(p => p.Name)
              .NotEmpty().WithMessage("{Name} is required.")
              .NotNull()
              .MaximumLength(250).WithMessage("{Name} must not be over 250 characters.");
        }
    }
}
