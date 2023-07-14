using FluentValidation;

namespace WebApplication1.Dtos.User
{
    public class LoginDto
    {
       
        public string? UserNameOrEmail { get; set; }
 
        public string? Password { get; set; }

    }

    class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
           
            RuleFor(u => u.UserNameOrEmail).NotEmpty().WithMessage("Please enter a username.");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Please enter a password.");     
        } 
    }
}
