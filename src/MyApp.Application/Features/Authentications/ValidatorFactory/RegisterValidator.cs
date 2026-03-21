using FluentValidation;
using MyApp.Application.Features.Authentications.Requests;
using MyApp.Application.Features.Products.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Authentications.ValidatorFactory
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không hợp lệ")
                .MaximumLength(256);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password không được để trống")
                .MinimumLength(8).WithMessage("Password phải ít nhất 8 ký tự")
                .MaximumLength(100);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("ConfirmPassword không được để trống")
                .Equal(x => x.Password)
                .WithMessage("ConfirmPassword không khớp Password");
        }
    }

}
