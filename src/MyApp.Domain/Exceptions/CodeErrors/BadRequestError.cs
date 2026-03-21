

namespace MyApp.Domain.Exceptions.CodeErrors
{
    public static class BadRequestError
    {
        public const string Default = ErrorCodeCategories.BadRequest;

        public const string InvalidCredentials = "INVALID_CREDENTIALS";

        public const string AccountLocked = "ACCOUNT_LOCKED";

        public const string NotAllowed = "NOT_ALLOWED";

        public const string EmailExists = "EMAIL_EXISTS";

        public const string InvalidInput = "INVALID_INPUT";
    }
}
