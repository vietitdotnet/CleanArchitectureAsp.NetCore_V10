
using MyApp.Domain.Exceptions.CodeErrors;
namespace MyApp.Domain.Exceptions
{
    public class BadRequestException : BaseException
    {
       public BadRequestException(
           string message = "Yêu cầu Không hợp lệ", 
           string errorCode = BadRequestError.Default) :
           base(message, errorCode)
        {

        }
    }

}
