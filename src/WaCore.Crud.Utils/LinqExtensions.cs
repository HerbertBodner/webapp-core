using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WaCore.Crud.Utils
{
    public static class LinqExtensions
    {
        // Took from
        // http://stackoverflow.com/questions/16013807/unable-to-sort-with-property-name-in-linq-orderby
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string sortField, bool @ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var exp = Expression.Lambda(prop, param);
            string method = @ascending ? "OrderBy" : "OrderByDescending";
            var types = new[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}
