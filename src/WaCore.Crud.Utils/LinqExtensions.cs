using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WaCore.Crud.Utils.Sorting;

namespace WaCore.Crud.Utils
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Sort an IQueryable by a sort string containing one or more sort fields.
        /// <para>
        ///     The <paramref name="sortString"/> parameter must contain one or more comma-separated sort fields. Sort order is defined by prepending a sort field with '+' (ascending) or '-' (descending). Default sort order is ascending.
        ///     The mapping of sort field names to expressions is provided in parameter <paramref name="sortFieldMapping"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q"></param>
        /// <param name="sortString"></param>
        /// <param name="sortFieldMapping">Mapping of sort field names to element member expressions specifying the sort keys. Use <see cref="Sorting.SortFieldMappingBuilder{TEntity}" /> to create a mapping using a Fluent API. </param>
        /// <returns>New IQueryable instance with applied sorting</returns>
        public static IQueryable<T> OrderBySortString<T>(this IQueryable<T> q, string sortString, SortFieldMapping<T> sortFieldMapping)
        {
            var orderList = SortBySplitter.SplitSortByString(sortString);
            orderList.Reverse();

            foreach (var orderItem in orderList)
            {
                var sortColumnDescriptors = sortFieldMapping.GetValueOrDefault(orderItem.FieldName);
                if (sortColumnDescriptors == null)
                {
                    throw new ArgumentException($"Invalid sort field \"{orderItem.FieldName}\".", sortString);
                }
                else
                {
                    foreach (var sortDescriptor in sortColumnDescriptors.Reverse())
                    {
                        var asc = sortDescriptor.Asc ^ orderItem.OrderDirection == OrderItem.OrderBy.Descending;
                        var methodName = asc ? "OrderBy" : "OrderByDescending";
                        var types = new[] { q.ElementType, sortDescriptor.KeyType };
                        var mce = Expression.Call(typeof(Queryable), methodName, types, q.Expression, sortDescriptor.KeySelector);
                        q = q.Provider.CreateQuery<T>(mce);
                    }
                }
            }
            return q;
        }
    }
}
