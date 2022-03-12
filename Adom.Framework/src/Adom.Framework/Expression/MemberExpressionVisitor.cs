
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Adom.Framework.Expressions
{
    public class MemberExpressionVisitor<TPublicEntity> : ExpressionVisitor
         where TPublicEntity : class
    {
        public MemberExpressionVisitor(ParameterExpression parameter)
        {
            Contract.Requires(parameter != null);

            this.Parameter = parameter!;
        }

        public ParameterExpression Parameter { get; set; }

        public Expression Replace(Expression expression)
        {
            return this.Visit(expression);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.ReflectedType != typeof(TPublicEntity))
            {
                return base.VisitMember(node);
            }

            var result = Expression.Property(
              this.Parameter,
              node.Member.Name);

            return result;
        }
    }
}
