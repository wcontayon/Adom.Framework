
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Adom.Framework.Expressions
{
    public class SubstituteExpressionVisitor : System.Linq.Expressions.ExpressionVisitor
    {
        private Dictionary<Expression, Expression> subst = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression newValue;
            if (this.subst.TryGetValue(node, out newValue!))
            {
                return newValue;
            }

            return node;
        }

        public Dictionary<Expression, Expression> SubstitueParameters { get => this.subst; }
    }
}
