

namespace MyApp.Domain.Exceptions.CodeErrors
{
    public static class ErrorCodeCategories
    {

        public const string RedirectToAction = "REDIRECT_ACTION";

        public const string BadRequest = "BAD_REQUEST";

        public const string Unauthorized = "UNAUTHORIZED";

        public const string Forbidden = "FORBIDDEN";

        public const string NotFound = "NOT_FOUND";

        public const string Validation = "VALIDATION_ERROR";

        public const string Database = "DATABASE";

        public const string General = "GENERAL";

        public const string Unknown = "Unknown";
    }
}
