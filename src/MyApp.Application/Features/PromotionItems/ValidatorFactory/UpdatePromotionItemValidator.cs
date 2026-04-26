using FluentValidation;
using MyApp.Application.Features.PromotionItems.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.PromotionItems.ValidatorFactory
{
    public class UpdatePromotionItemValidator : AbstractValidator<UpdatePromotionItemRequest>
    {
        public UpdatePromotionItemValidator()
    {
        // ===== 1. OriginalPrice > 0 =====
        RuleFor(x => x.OriginalPrice)
            .GreaterThan(0)
            .WithMessage("Giá gốc phải lớn hơn 0");

        // ===== 2. OverrideValue logic =====


        When(x => x.OverrideValue.HasValue, () =>
        {
            // Bắt buộc phải có IsPercentageOverride
            RuleFor(x => x.IsPercentageOverride)
                .NotNull()
                .WithMessage("Chọn loại giảm giá (% hoặc tiền)");

            // Nếu là %
            When(x => x.IsPercentageOverride == true, () =>
            {
                RuleFor(x => x.OverrideValue!.Value)
                    .InclusiveBetween(0, 100)
                    .WithMessage("Giảm theo % phải nằm trong khoảng 0 - 100");
            });

            // Nếu là tiền
            When(x => x.IsPercentageOverride == false, () =>
            {
                RuleFor(x => x.OverrideValue!.Value)
                      .Cascade(CascadeMode.Stop)
                      .NotEmpty().WithMessage("Giá trị giảm không được để trống.")
                          .Must(x => x > 0).WithMessage("Giá trị giảm phải lớn hơn 0.")
                        .LessThanOrEqualTo(10000000000)
                             .WithMessage("Giá trị giảm nhập không đúng.");
            });
        });

        // ===== 3. Trường hợp ngược lại =====
        // Nếu OverrideValue NULL thì IsPercentageOverride cũng phải NULL

        When(x => !x.OverrideValue.HasValue, () =>
        {
            RuleFor(x => x.IsPercentageOverride)
                .Null()
                .WithMessage("Không được set loại giảm giá khi không có giá trị giảm");
        });

        }


    }
}
