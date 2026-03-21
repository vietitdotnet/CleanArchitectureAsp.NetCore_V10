using FluentValidation;
using MyApp.Application.Extentions;
using MyApp.Application.Features.Products.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Products.ValidatorFactory
{
    public class CreateProductValidator
     : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Tên sản phẩm bắt buộc nhập")
                .MaximumLength(50)
                    .WithMessage("Tên sản phẩm không được vượt quá 50 ký tự");

            RuleFor(x => x.Slug)
                .NotEmpty()
                    .WithMessage("Slug bắt buộc nhập")
                .MaximumLength(50)
                    .WithMessage("Slug không được vượt quá 50  ký tự")
                .MustBeValidSlug();

            RuleFor(x => x.Description)
                .MaximumLength(380)
                    .WithMessage("Mô tả không được vượt quá 2000 ký tự");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                    .WithMessage("Giá sản phẩm phải lớn hơn 0");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                    .When(x => x.CategoryId.HasValue)
                    .WithMessage("CategoryId phải lớn hơn 0");

        }
    }
}
