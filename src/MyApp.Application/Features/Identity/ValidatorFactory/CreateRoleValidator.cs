using FluentValidation;
using MyApp.Application.Common.Validation.Extenttions;
using MyApp.Application.Features.Authentications.Requests;
using MyApp.Application.Features.Identity.Requests;
using MyApp.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Identity.ValidatorFactory
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleValidator() 
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Tên không được để trống")
             .MaximumLength(20).WithMessage("Tên tối đa 20 ký tự")
             .OnlyLetters();
        }
    }
}
