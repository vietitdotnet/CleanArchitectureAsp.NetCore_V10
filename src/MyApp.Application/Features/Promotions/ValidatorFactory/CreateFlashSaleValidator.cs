using FluentValidation;
using MyApp.Application.Features.Promotions.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Promotions.ValidatorFactory
{
    public class CreateFlashSaleValidator : BasePromotionValidator<CreateFlashSaleRequest>
    {
        public CreateFlashSaleValidator()
        {
            RuleFor(x => x.LimitQuantity)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Số lượng giới hạn không được để trống.")
                .GreaterThan(0).WithMessage("Số lượng giới hạn phải lớn hơn 0.");
            

            RuleFor(x => x.DailyStartTime)
                .NotEmpty()
                .WithMessage("Giờ bắt đầu không được để trống.");
           
            RuleFor(x => x.DailyEndTime)
                .NotEmpty().WithMessage("Giờ kết thúc không được để trống.")
                .GreaterThan(x => x.DailyStartTime).WithMessage("Giờ kết thúc phải sau giờ bắt đầu.");
        }
    }
}
