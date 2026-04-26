
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApp.Domain.Exceptions
{
    public class IdentityResultException : BaseException
    {
        public IEnumerable<string> Errors { get; }
        public IEnumerable<string> Codes { get; }

        public IdentityResultException(
            IEnumerable<string> errors,
            IEnumerable<string> codes,
            string message = "Identity error",
            string errorCode = "IDENTITY_ERROR")
            : base(message, errorCode)
        {
            Errors = errors;
            Codes = codes;
        }
    }


}
