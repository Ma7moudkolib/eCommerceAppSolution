using eCommerce.Application.DTOs.Identity;
using FluentValidation;
namespace eCommerce.Application.Validations.Authentication
{
    public class LoginUserValidator : AbstractValidator<LoginUser>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required.")
               .EmailAddress().WithMessage("Invalid Email format.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required.");
        }

    }

}
