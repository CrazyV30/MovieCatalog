using backEnd.Contracts;
using FluentValidation;

namespace backEnd.Validators
{
    public class RegisterValidator:AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Uncorrect email");
            RuleFor(x => x.Surname).NotEmpty().Length(2,20).WithMessage("Too long Surname");
            RuleFor(x => x.Username).NotEmpty().Length(1,20).WithMessage("Too long Surname");
            RuleFor(x => x.Password).NotEmpty().Length(6, 20).WithMessage("Too short or too long password");
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password).WithMessage("ConfPassword must be equal to Password");
        }
    }
}
