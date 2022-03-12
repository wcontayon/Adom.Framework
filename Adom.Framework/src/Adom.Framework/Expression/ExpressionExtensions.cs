using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Adom.Framework.Expressions
{
    public static class ExpressionExtensions
    {
        public static Expression ReplaceExpression<TDataEntity, TPublicEntity>(this MemberExpressionVisitor<TPublicEntity> visitor, Expression filterExpression)
            where TPublicEntity : class, new()
        {
            Contract.Requires(filterExpression != null);
            Contract.Ensures(Contract.Result<Expression>() != null);

            var methodCallExpressionFilter = filterExpression as MethodCallExpression;
            Contract.Assert(methodCallExpressionFilter != null);
            Contract.Assert(methodCallExpressionFilter.Arguments.Count == 2);

            var unquotedLambdaExpressionFilter = visitor.Replace(methodCallExpressionFilter.Arguments[1])
              .Unquote() as LambdaExpression;
            Contract.Assert(unquotedLambdaExpressionFilter != null);

            var expressionLambda = Expression.Lambda<Func<TDataEntity, bool>>(unquotedLambdaExpressionFilter.Body, visitor.Parameter);
            return expressionLambda;
        }

        public static Expression Unquote(this Expression quote)
        {
            return quote.NodeType == ExpressionType.Quote
            ? ((UnaryExpression)quote).Operand.Unquote()
            : quote;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {
            ParameterExpression p = a.Parameters[0];

            SubstituteExpressionVisitor visitor = new SubstituteExpressionVisitor();
            visitor.SubstitueParameters[b.Parameters[0]] = p;

            Expression body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {
            ParameterExpression p = a.Parameters[0];

            SubstituteExpressionVisitor visitor = new SubstituteExpressionVisitor();
            visitor.SubstitueParameters[b.Parameters[0]] = p;

            Expression body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }
    }
}
