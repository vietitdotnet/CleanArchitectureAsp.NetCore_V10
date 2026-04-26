using FluentValidation;
using MyApp.Application.Common.Validation.Extenttions;
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
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Tên không được để trống.")
                .MinimumLength(3).WithMessage("Tên sản phẩm phải có ít nhất 3 ký tự.")
                .MaximumLength(100).WithMessage("Tên sản phẩm không được vượt quá 150 ký tự.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(380)
                    .WithMessage("Mô tả không được vượt quá 380 ký tự.");

            RuleFor(x => x.Price)
                .Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage("Giá nhập không được để trống.")
                      .Must(x => x > 0).WithMessage("Giá nhập phải lớn hơn 0.")
                    .LessThanOrEqualTo(10000000000)
                        .WithMessage("Giá trị nhập không đúng.");

        }
    }
}
