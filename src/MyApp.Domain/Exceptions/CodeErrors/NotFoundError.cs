namespace MyApp.Domain.Exceptions.CodeErrors
{
    public static class NotFoundError
    {
        public const string Default = ErrorCodeCategories.NotFound  ;
        public const string UserNotFound = "USER_NOT_FOUND";
        public const string RoleNotFound = "ROLE_NOT_FOUND";
        public const string RecordNotFound = "RECORD_NOT_FOUND";
    }
}
