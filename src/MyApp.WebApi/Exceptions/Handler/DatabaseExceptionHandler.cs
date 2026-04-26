using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyApp.WebApi.Exceptions.Models;
using System.Data.Common;

namespace MyApp.WebApi.Exceptions.Handler
{
    internal sealed class DatabaseExceptionHandler : BaseExceptionHandler<Exception>
    {
        public DatabaseExceptionHandler(ILogger<DatabaseExceptionHandler> logger)
            : base(logger) { }

        protected override ApiProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
        {
            var problem = new ApiProblemDetails
            {
                TraceId = context.TraceIdentifier

            };

            switch (exception)
            {
                case SqlException sqlEx:
                    
                    problem.Status = StatusCodes.Status500InternalServerError;
                    problem.Title = "Database Error";
                    problem.ErrorCode = "DATABASE_ERROR";
                    problem.ErrorMessage = MapSqlError(sqlEx);
                    break;

                case NullReferenceException nullEx:
                    problem.Status = StatusCodes.Status500InternalServerError;
                    problem.Title = "Database Null Reference Error";
                    problem.ErrorCode = "DB_NULL_REFERENCE_ERROR";
                    problem.ErrorMessage = nullEx.InnerException?.Message ?? "Lỗi khi ghi dữ liệu vào cơ sở dữ liệu.";
                    break;

                case DbUpdateException dbUpdateEx:
                    problem.Status = StatusCodes.Status500InternalServerError;
                    problem.Title = "Database Update Error";
                    problem.ErrorCode = "DB_UPDATE_ERROR";
                    problem.ErrorMessage = dbUpdateEx.InnerException?.Message
                        ?? "Lỗi khi ghi dữ liệu vào cơ sở dữ liệu.";
                    break;

                case TimeoutException:
                    problem.Status = StatusCodes.Status504GatewayTimeout;
                    problem.Title = "Database Timeout";
                    problem.ErrorCode = "DB_TIMEOUT";
                    problem.ErrorMessage = "Kết nối hoặc truy vấn cơ sở dữ liệu quá thời gian cho phép.";
                    break;

                case DbException dbEx:
                    problem.Status = StatusCodes.Status500InternalServerError;
                    problem.Title = "Database Error";
                    problem.ErrorCode = "DATABASE_ERROR";
                    problem.ErrorMessage = dbEx.Message;
                    break;

                default:          
                    break;
            }

            return problem;
        }

        private static string MapSqlError(SqlException sqlEx)
        {
            return sqlEx.Number switch
            {
                2601 or 2627 => "Dữ liệu bị trùng lặp (vi phạm ràng buộc unique hoặc primary key).",
                547 => "Không thể xóa hoặc cập nhật dữ liệu vì có ràng buộc khóa ngoại.",
                1205 => "Deadlock trong cơ sở dữ liệu. Vui lòng thử lại.",
                _ => "Lỗi truy vấn cơ sở dữ liệu. Vui lòng thử lại sau."
            };
        }
    }

}
