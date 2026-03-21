using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Exceptions.CodeErrors
{
    public static class RedirectCodes
    {

        public const string Default = ErrorCodeCategories.RedirectToAction;

        public const string EmailNotConfirmed = "EMAIL_NOT_CONFIRMED";

        public const string PhoneNotVerified = "PHONE_NOT_VERIFIED";

        public const string PaymentPending = "PAYMENT_PENDING";

        public const string ProfileIncomplete = "PROFILE_INCOMPLETE";
    }
}
