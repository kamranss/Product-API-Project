using FluentValidation;
using System.Data;

namespace WebApplication1.Dtos.User
{
    public class UserRegisterDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; }

        public class RegisterDtoValidator : AbstractValidator<UserRegisterDto>
        {
            public RegisterDtoValidator()
            {
                ApplyCommonValidationRules();
                RuleFor(u => u.UserName).NotEmpty().WithMessage("Please enter a username.");
                RuleFor(u => u.Name).NotEmpty().WithMessage("Please enter a name.");
                RuleFor(u => u.Surname).NotEmpty().WithMessage("Please enter a surname.");
                RuleFor(u => u.Email).NotEmpty().WithMessage("Please enter an email address.").EmailAddress().WithMessage("Please enter a valid email address.");
                RuleFor(u => u.Password).NotEmpty().WithMessage("Please enter a password.");
                RuleFor(u => u.ConfirmedPassword).NotEmpty().WithMessage("Please confirm your password.")
                    .Must((dto, confirmedPassword) => confirmedPassword == dto.Password)
                    .WithMessage("Confirmed password must match the password.");
            }

            private void ApplyCommonValidationRules()
            {
                RuleFor(u => u.UserName).Length(3, 20).WithMessage("Username length should be between 3 and 20 characters.");
                RuleFor(u => u.Name).Length(3, 20).WithMessage("Name length should be between 3 and 20 characters.");
                RuleFor(u => u.Surname).Length(3, 20).WithMessage("Surname length should be between 3 and 20 characters.");
                RuleFor(u => u.Password).Length(3, 20).WithMessage("Password length should be between 3 and 20 characters.");
                RuleFor(u => u.ConfirmedPassword).Length(3, 20).WithMessage("Confirmed password length should be between 3 and 20 characters.");
            }
        }

    }
}
