namespace MyApp.Application.Common.Results
{
    public class OperationResult<T>
    {
        public bool Success { get; private set; }

        public string? Message { get; private set; }

        public T Data { get; private set; } = default!;

        public Dictionary<string, string[]>? Errors { get; private set; }

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
                Message = error,  
            };
        }

        public static OperationResult<T> Fails(IEnumerable<string> errors , string? message = null)
        {
            var list = errors.ToArray();

                return new OperationResult<T>
                {
                    Success = false,
                    Message = message ?? "One or more errors occurred.",
                    Errors = new Dictionary<string, string[]>
                {
                    { "general", list }
                }
            };
        }

        public static OperationResult<T> Fails(Dictionary<string, string[]> errors, string? message = null)
        {
            return new OperationResult<T>
            {
                Success = false,
                Message = message ?? "One or more errors occurred.",
                Errors = errors
            };
        }
    }

}