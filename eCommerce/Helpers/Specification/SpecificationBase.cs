using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace eCommerce.Helpers.Specification
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        private Func<T, bool> _compiledExpression;
 
        private Func<T, bool> CompiledExpression
        {
            get { return _compiledExpression ?? (_compiledExpression = SpecExpression.Compile()); }
        }
 
        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }
    }
}
