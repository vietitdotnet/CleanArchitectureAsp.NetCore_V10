using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Extentions
{
    public static class CustomValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeValidSlug<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(@"^[a-z0-9]+(-[a-z0-9]+)*$")
                .WithMessage("Slug không hợp lệ. Slug chỉ được chứa chữ cái thường, số và dấu gạch ngang (-).");
        }
    }
}
