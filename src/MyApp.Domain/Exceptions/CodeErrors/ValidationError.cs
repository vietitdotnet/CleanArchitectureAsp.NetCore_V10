
namespace MyApp.Domain.Exceptions.CodeErrors
{
    public static class ValidationError
    {
        public const string Default = ErrorCodeCategories.Validation;

        public const string InvalidFormat = "INVALID_FORMAT";

        public const string MissingField = "MISSING_FIELD";
    }
}
