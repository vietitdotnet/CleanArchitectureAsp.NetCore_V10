using FluentValidation;
using MyApp.Application.Features.Products.Requests;
using MyApp.Application.Features.ProductUints.Repuests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.ProductUints.ValidatorFactory
{
    public class CreateProductUnitValidator : AbstractValidator<CreateProductUnitRequest>
    {
        public CreateProductUnitValidator()
        {
            RuleFor(x => x.UnitName)
           .Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage("Tên đơn vị không được để trống.")
           .MaximumLength(100).WithMessage("Tên đơn vị không vượt quá 100 ký tự.");

            RuleFor(x => x.Barcode)
                .MaximumLength(20).WithMessage("Mã vạch không vượt quá 20 ký tự.");

            RuleFor(x => x.SellingPrice)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Giá bán không được để trống.")
                .GreaterThan(0).WithMessage("Giá bán phải lớn hơn 0.")
                .LessThanOrEqualTo(10000000000)
                    .WithMessage("Giá bán không hợp lệ.");
        }
    }
}
