using MyApp.Domain.Exceptions.CodeErrors;

namespace MyApp.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
            public NotFoundException(
                string message = "Không tìm thấy tài nguyên yêu cầu.", 
                string errorCode = NotFoundError.Default)
                : base(message, errorCode)
            {
            }
        }
    }


