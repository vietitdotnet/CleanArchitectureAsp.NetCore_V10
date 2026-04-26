using MyApp.Domain.Exceptions.CodeErrors;

namespace MyApp.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {

        public NotFoundException(
                string message = "Không tìm thấy tài nguyên yêu cầu.", 
                string errorCode = "Not_Found")
                : base(message, errorCode)
            {
            }
        }
    }


