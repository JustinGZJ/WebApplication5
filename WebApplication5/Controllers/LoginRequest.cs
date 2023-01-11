using FluentValidation;

namespace WebApplication5.Controllers;

public record LoginRequest(string UserName, string Password);

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull().WithMessage("UserName is required")
            .Length(3, 6)
            .WithMessage("长度必须在3-6之间");
        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage("Password is required")
            .Length(3, 10)
            .WithMessage("密码长度必须介于3到10之间");
    }
}