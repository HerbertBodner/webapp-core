using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace WaCore.Crud.Utils.Sorting
{
    public class SortFieldMapping<TEntity> : Dictionary<string, SortColumnDescriptor<TEntity>[]>
    {
        public SortFieldMapping(IEqualityComparer<string> comparer) : base(comparer)
        { }

        public SortFieldMapping() : base()
        { }
    }


    public class SortColumnDescriptor<TEntity>
    {
        public SortColumnDescriptor<TEntity> Initialize<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool asc)
        {
            KeyType = typeof(TKey);
            KeySelector = keySelector;
            Asc = asc;
            return this;
        }

        public Type KeyType { get; set; }
        // This should be of type Expression<Func<TEntity,KeyType>>
        public Expression KeySelector { get; set; }
        public bool Asc { get; set; }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> q, bool reverseOrder)
        {
            var asc = Asc ^ reverseOrder;
            var methodName = asc ? "OrderBy" : "OrderByDescending";
            var types = new[] { q.ElementType, KeyType };
            var mce = Expression.Call(typeof(Queryable), methodName, types, q.Expression, KeySelector);
            return q.Provider.CreateQuery<TEntity>(mce);
        }
    }
}
