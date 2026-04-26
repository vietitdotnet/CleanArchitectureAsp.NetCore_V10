using FluentValidation;
using MyApp.Application.Features.Promotions.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.ValidatorFactory
{
    public class BasePromotionValidator<T> : AbstractValidator<T> where T : CreatePromotionBase
    {
        public BasePromotionValidator()
        {
            RuleFor(x => x.Name)
              .Cascade(CascadeMode.Stop)
              .NotEmpty().WithMessage("Tên không được để trống.")
              .MinimumLength(3).WithMessage("Tên phải có ít nhất 3 ký tự.")
              .MaximumLength(100).WithMessage("Tên sản phẩm không được vượt quá 150 ký tự.");
            
            RuleFor(x => x.Value)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Giá trị giảm không được để trống.")
                .GreaterThan(0)
                .LessThanOrEqualTo(1000000000)
                .WithMessage("Giá trị giảm nhập không đúng.");

            RuleFor(x => x.StartDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ngày bắt đầu không được để trống.")
                .LessThan(x => x.EndDate).WithMessage("Ngày bắt đầu phải trước kết thúc.");


            RuleFor(x => x.EndDate)
               .Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("Ngày kết thúc  không được để trống.");
              

            When(x => x.IsPercentage, () => {
                RuleFor(x => x.Value)
                .LessThanOrEqualTo(100).WithMessage("Phần trăm không quá 100%");
                
                RuleFor(x => x.MaxDiscountAmount)
                .Cascade(CascadeMode.Stop)
                 .NotEmpty().WithMessage("Giảm tối đa không được để trống.")
                      .Must(x => x > 0).WithMessage("Giảm tối đa phải lớn hơn 0.")
                    .LessThanOrEqualTo(10000000000)
                        .WithMessage("Giảm tối đa nhập không đúng.");
            
            }).Otherwise(() => {
                RuleFor(x => x.MaxDiscountAmount)
                .Null()
                .WithMessage("Tiền mặt không dùng MaxDiscountAmount");
            });
        }


    }
}
