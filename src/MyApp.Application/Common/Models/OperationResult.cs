namespace MyApp.Application.Common.Models
{
    public class OperationResult<T>
    {
        public bool Success { get; private set; }

        public string? Message { get; private set; }

        public T Data { get; private set; } = default!;

        public IEnumerable<string>? Errors { get; private set; }

        private OperationResult() { }

        public static OperationResult<T> Ok(T data, string? message = null)
        {
            return new OperationResult<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }

        public static OperationResult<T> Fail(string error)
        {
            return new OperationResult<T>
            {
                Success = false,
                Errors = new[] { error }
            };
        }

        public static OperationResult<T> Fail(IEnumerable<string> errors)
        {
            return new OperationResult<T>
            {
                Success = false,
                Errors = errors
            };
        }
    }

}
