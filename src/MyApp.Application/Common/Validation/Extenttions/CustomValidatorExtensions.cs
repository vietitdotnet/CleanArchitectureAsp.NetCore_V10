using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Common.Validation.Extenttions
{
    public static class CustomValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValidSlug<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^[a-z0-9]+(-[a-z0-9]+)*$")
                .WithMessage("Slug không hợp lệ. Slug chỉ được chứa chữ cái thường, số và dấu gạch ngang (-).");
        }

        public static IRuleBuilderOptions<T, string?> MustBeValidSku<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^[A-Z0-9_-]+$")
                .WithMessage("SKU không hợp lệ. Chỉ gồm chữ hoa, số, dấu '-' hoặc '_'.");
        }

        public static IRuleBuilderOptions<T, string?> MustBeValidBarcode<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^\d{8}$|^\d{12}$|^\d{13}$")
                .WithMessage("Mã vạch không hợp lệ (chỉ chứa số, 8/12/13 chữ số).");
        }

        public static IRuleBuilderOptions<T, string?> MustBeValidCountryCode<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .Matches(@"^[A-Z]{2}$|^[A-Z]{3}$")
                .WithMessage("Code phải gồm 2 hoặc 3 ký tự chữ in hoa (VD: VN, VNM, US, USA).");
        }

        public static IRuleBuilderOptions<T, string> OnlyLetters<T>(
        this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches("^[a-zA-Z]+$")
                .WithMessage("Chỉ được chứa chữ cái a-z hoặc A-Z, không có khoảng trắng");
        }
    }
}
