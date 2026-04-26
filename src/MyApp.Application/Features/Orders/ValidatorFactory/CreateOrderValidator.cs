using FluentValidation;
using MyApp.Application.Features.Orders.Requests;
using MyApp.Application.Features.Products.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Orders.ValidatorFactory
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            // Note (optional)
            RuleFor(x => x.Note)
                .MaximumLength(100).WithMessage("Ghi chú không quá 100 ký tự");

            // Province
            RuleFor(x => x.ProvinceCode)
                .NotEmpty().WithMessage("Province is required")
                .MaximumLength(20);

            // Commune
            RuleFor(x => x.CommuneCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Commune is required")
                .MaximumLength(20);

            // Address detail
            RuleFor(x => x.AddressDetail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Address detail is required")
                .MaximumLength(100).WithMessage("Địa chỉ cụ thể không quá 100 ký tự");

            // Customer name
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required")
                .MaximumLength(100);

            // Phone number
            RuleFor(x => x.PhoneNumber)
                 .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .Matches(@"^(0|\+84)[0-9]{9,10}$")
                .WithMessage("Số điện thoại không hợp lệ.");

            // Email (optional nhưng nếu có thì phải đúng format)
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Email không hợp lệ.");

            // Items
            RuleFor(x => x.Items)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Các mục là bắt buộc")
                .Must(x => x.Any()).WithMessage("Đơn hàng phải có ít nhất một mặt hàng");

            // Validate từng item
            RuleForEach(x => x.Items).SetValidator(new CreateOrderItemValidator());
        }
    }

}
