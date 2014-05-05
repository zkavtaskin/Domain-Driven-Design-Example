using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace eCommerce.Helpers.Specification
{
    public class Or<T> : SpecificationBase<T>
    {
        ISpecification<T> left;
        ISpecification<T> right;

        public Or(
            ISpecification<T> left,
            ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        // OrSpecification
        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(
                        Expression.Invoke(left.SpecExpression, objParam),
                        Expression.Invoke(right.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
