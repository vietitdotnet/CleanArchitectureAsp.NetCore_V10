using FluentValidation;
using MyApp.Application.Features.Orders.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Orders.ValidatorFactory
{
    public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemRequest>
    {
        public CreateOrderItemValidator()
        {

            RuleFor(x => x.Quantity)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0.")
                .LessThanOrEqualTo(1000).WithMessage("Lỗi nhập quá số lượng cho phép.");
        }
    }
}
