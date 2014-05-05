using eCommerce.Helpers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.Helpers.Specification
{
    public static class IExtensions
    {
        public static ISpecification<T> And<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            return new And<T>(left, right);
        }

        public static ISpecification<T> Or<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            return new Or<T>(left, right);
        }

        public static ISpecification<T> Negate<T>(this ISpecification<T> inner)
        {
            return new Negated<T>(inner);
        }
    }
}
