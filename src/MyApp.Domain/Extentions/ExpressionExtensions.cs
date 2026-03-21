using System.Linq.Expressions;

namespace MyApp.Domain.Extentions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var leftVisitor = new ReplaceExpressionVisitor(left.Parameters[0], parameter);
            var leftBody = leftVisitor.Visit(left.Body);

            var rightVisitor = new ReplaceExpressionVisitor(right.Parameters[0], parameter);
            var rightBody = rightVisitor.Visit(right.Body);

            var body = Expression.AndAlso(leftBody!, rightBody!);

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

    internal sealed class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression? Visit(Expression? node)
            => node == _oldValue ? _newValue : base.Visit(node);
    }
}
