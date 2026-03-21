

using MyApp.Domain.Exceptions.CodeErrors;

namespace MyApp.Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string ErrorCode { get; }

        protected BaseException(
            string message = "Đã xảy ra lỗi không xác định.",
            string errorCode = ErrorCodeCategories.Unknown)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }

}
