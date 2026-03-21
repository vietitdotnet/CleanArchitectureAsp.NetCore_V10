

namespace MyApp.Domain.Exceptions.CodeErrors
{
    public static class DataBaseError
    {

        public const string Default = ErrorCodeCategories.Database;

        public const string TransactionFailed = "TRANSACTION_FAILED";

        public const string ConnectionFailed = "DB_CONNECTION_FAILED";
    }
}
