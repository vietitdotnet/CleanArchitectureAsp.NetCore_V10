using FluentValidation;
using MyApp.Application.Common.Validation.Extenttions;
using MyApp.Application.Features.Products.Requests;
using MyApp.Application.Features.ProductUints.ValidatorFactory;
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
            // Khi Sku không trống thì mới áp dụng các luật bên trong
            When(x => !string.IsNullOrWhiteSpace(x.Sku), () =>
            {
                RuleFor(x => x.Sku)
                    .MinimumLength(5).WithMessage("Mã hàng phải có ít nhất 5 ký tự.")
                    .MaximumLength(20).WithMessage("Mã hàng không vượt quá 20 ký tự.")
                    .MustBeValidSku();
            });

            When(x => !string.IsNullOrWhiteSpace(x.Barcode), () =>
            {
                RuleFor(x => x.Barcode)
                    .MinimumLength(6).WithMessage("Mã vạch phải có ít nhất 6 ký tự.")
                    .MaximumLength(30).WithMessage("Mã vạch không vượt quá 30 ký tự.")
                    .MustBeValidBarcode();
            });

            /*
              RuleFor(x => x.Slug)
             .Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage("Slug không được để trống.")
             .MaximumLength(100).WithMessage("Slug không vượt quá 100 ký tự.");
            */

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Tên hiển thị không được để trống.")
                .MinimumLength(3).WithMessage("Tên hiển thị phải có ít nhất 3 ký tự.")
                .MaximumLength(200).WithMessage("Tên hiển thị không được vượt quá 200 ký tự.");

            RuleFor(x => x.ShortName)
               .Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("Tên ngắn không được để trống.")
               .MinimumLength(3).WithMessage("Tên ngắn phải có ít nhất 3 ký tự.")
               .MaximumLength(100).WithMessage("Tên ngắn không được vượt quá 100 ký tự.");

            RuleFor(x => x.PackingSize)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Quy cách đóng gói không được để trống.")
                .MinimumLength(3).WithMessage("Quy cách đóng gói phải có ít nhất 3 ký tự.");

            RuleFor(x => x.ShortDescription)
                .MaximumLength(38)
                .WithMessage("Mô tả ngắn không vượt quá 38 ký tự.");

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .WithMessage("Mô tả không được vượt quá 2000 ký tự.");

            RuleFor(x => x.RegistrationNumber)
                .MaximumLength(20).WithMessage("Số đăng ký không vượt quá 20 ký tự.");

            RuleFor(x => x.Benefit)
                .MaximumLength(150).WithMessage("Công dụng không vượt quá 150 ký tự.");

            RuleFor(x => x.DosageForm)
                .MaximumLength(100).WithMessage("Dạng bào chế không vượt quá 100 ký tự.");

            RuleFor(x => x.Ingredient)
                .MaximumLength(100).WithMessage("Thành phần không vượt quá 100 ký tự.");

            // CostPrice (theo DB constraint)
            RuleFor(x => x.CostPrice)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Giá nhập không được để trống.")
                .GreaterThan(0).WithMessage("Giá nhập phải lớn hơn 0.")
                .LessThanOrEqualTo(100000000)
                .WithMessage("Giá nhập không hợp lệ.");

            //  BasePrice
            RuleFor(x => x.BasePrice)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Giá bán niêm yết không được để trống.")
                .GreaterThan(0).WithMessage("Giá bán phải lớn hơn 0.")
                .LessThanOrEqualTo(10000000000)
                .WithMessage("Giá bán không hợp lệ.");

            RuleFor(x => x.UnitName)
                      .Cascade(CascadeMode.Stop)
                      .NotEmpty().WithMessage("Tên đơn vị không được để trống.")
                      .MaximumLength(100).WithMessage("Tên đơn vị không vượt quá 100 ký tự.");

       
            RuleFor(x => x.SellingPrice)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Giá bán không được để trống.")
                .GreaterThan(0).WithMessage("Giá bán phải lớn hơn 0.")
                .LessThanOrEqualTo(10000000000)
                    .WithMessage("Giá bán không hợp lệ.");

        }
    }
}
