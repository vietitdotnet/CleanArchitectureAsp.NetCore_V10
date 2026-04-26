using System.Linq.Expressions;

namespace MyApp.Domain.Core.Specifications
{
    /// <summary>
    /// Lớp cơ sở cho Specification — dùng để mô tả cách truy vấn dữ liệu một cách linh hoạt.
    /// </summary>
    public class BaseSpecification<T> : ISpecification<T>
    {

        protected BaseSpecification()
        { 
            Criteria = x => true;
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria ?? (x => true);
        }

        public Expression<Func<T, bool>> Criteria { get; protected set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public List<string> IncludeStrings { get; } = new();
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        public Expression<Func<T, object>>? GroupBy { get; private set; } 
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;
        public bool AsNoTracking { get; private set; } = false;


        #region Include
        public void AddInclude(Expression<Func<T, object>> includeExpression)
            => Includes.Add(includeExpression);

        public void AddInclude(string includeString)
            => IncludeStrings.Add(includeString);
        #endregion

        #region OrderBy / OrderByDescending / GroupBy
        public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
            => OrderBy = orderByExpression;

        public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
            => OrderByDescending = orderByDescendingExpression;

        public void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
            => GroupBy = groupByExpression;
        #endregion

        #region  Paging
        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        #endregion

        #region Tracking
        /// <summary>
        /// Kích hoạt hoặc tắt chế độ tracking
        /// </summary>
        public void ApplyAsNoTracking(bool value = true)
            => AsNoTracking = value;
        #endregion

        #region  Dynamic Sort (tùy chọn)
        /// <summary>
        /// Cho phép sắp xếp động theo tên property.
        /// </summary>
        public void ApplyOrderBy(string propertyName, bool ascending = true)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var converted = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(converted, parameter);

            if (ascending)
                ApplyOrderBy(lambda);
            else
                ApplyOrderByDescending(lambda);
        }
        #endregion
    }
}
