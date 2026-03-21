
namespace MyApp.Domain.Exceptions.CodeErrors
{
    public static class UnauthorizedError
    {
        public const string Default = ErrorCodeCategories.Unauthorized;

        public const string TokenExpired = "TOKEN_EXPIRED";

        public const string InvalidToken = "INVALID_TOKEN";
    }
}
