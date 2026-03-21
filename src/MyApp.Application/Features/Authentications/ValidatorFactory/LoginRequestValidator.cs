using FluentValidation;
using MyApp.Application.Features.Authentications.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Authentications.ValidatorFactory
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ")
                .MaximumLength(50);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password không được để trống")
                .MinimumLength(8).WithMessage("Password phải ít nhất 8 ký tự")
                .MaximumLength(50);
        }
    }
}
