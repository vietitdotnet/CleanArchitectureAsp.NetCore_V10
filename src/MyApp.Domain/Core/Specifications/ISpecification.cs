using System.Linq.Expressions;

namespace MyApp.Domain.Core.Specifications
{
    public interface ISpecification<T>
    {
        /// <summary>
        /// Biểu thức điều kiện lọc (WHERE)
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Bao gồm các navigation properties cần Include
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Các include dạng string ("OrderProducts.Product")
        /// </summary>
        List<string> IncludeStrings { get; }

        /// <summary>
        /// Sắp xếp tăng dần
        /// </summary>
        Expression<Func<T, object>>? OrderBy { get; }

        /// <summary>
        /// Sắp xếp giảm dần
        /// </summary>
        Expression<Func<T, object>>? OrderByDescending { get; }

        /// <summary>
        /// Gom nhóm (GroupBy)
        /// </summary>
        Expression<Func<T, object>>? GroupBy { get; }

        /// <summary>
        /// Số lượng phần tử sẽ lấy (Take)
        /// </summary>
        int Take { get; }

        /// <summary>
        /// Số lượng phần tử sẽ bỏ qua (Skip)
        /// </summary>
        int Skip { get; }

        /// <summary>
        /// Có áp dụng phân trang hay không
        /// </summary>
        bool IsPagingEnabled { get; }

        /// <summary>
        /// Có bật AsNoTracking (truy vấn chỉ đọc) hay không
        /// </summary>
        bool AsNoTracking { get; }
    }
}
