using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace WaCore.Crud.Utils.Sorting
{
    public class SortFieldMappingBuilder<TEntity> : ISortFieldMappingBuilder<TEntity>
    {
        private List<SingleSortFieldMap<TEntity>> _sortMappings = new List<SingleSortFieldMap<TEntity>>();

        public bool CaseSensitive { get; set; }

        public IFirstSortConfigurable<TEntity> ForSortField(string sortField)
        {
            var sortMapping = new SingleSortFieldMap<TEntity>(sortField);
            _sortMappings.Add(sortMapping);
            return sortMapping;
        }

        public IFirstSortConfigurable<TEntity> ForDtoSortField<TDto>(Expression<Func<TDto, object>> expr)
        {
            var body = expr.Body;
            // If the member is of a value type then the expression root is the operation of boxing it.
            if (body is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Convert)
            {
                body = unaryExpression.Operand;
            }

            var sortField = string.Empty;
            while (body != null)
            {
                if (body.NodeType == ExpressionType.Parameter)
                {
                    body = null;
                }
                else if (body.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpr = (MemberExpression)body;
                    sortField = $".{memberExpr.Member.Name}{sortField}";
                    body = memberExpr.Expression;
                }
                else
                    throw new ArgumentException($"{nameof(expr)} must contain only member access expressions but the tree contains a node of type {body.NodeType}. Expression: {expr.ToString()}", nameof(expr));
            }
            if (sortField == string.Empty)
                throw new ArgumentException($"{nameof(expr)} must contain a member access expression", nameof(expr));

            return ForSortField(sortField.TrimStart('.'));
        }

        public SortFieldMapping<TEntity> Build()
        {
            var sortFieldMapping = new SortFieldMapping<TEntity>(CaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
            foreach (var mapping in _sortMappings)
            {
                sortFieldMapping.Add(mapping.SortField, mapping.SortColumns.ToArray());
            }
            return sortFieldMapping;
        }
    }

    public class SingleSortFieldMap<TEntity> : IFirstSortConfigurable<TEntity>, ISecondarySortConfigurable<TEntity>
    {
        public string SortField { get; set; }

        public List<SortColumnDescriptor<TEntity>> SortColumns { get; set; } = new List<SortColumnDescriptor<TEntity>>();

        public SingleSortFieldMap(string sortField)
        {
            SortField = sortField;
        }

        public ISecondarySortConfigurable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> sortExpression)
        {
            SortColumns.Add(new SortColumnDescriptor<TEntity>().Initialize(sortExpression, asc: true));
            return this;
        }

        public ISecondarySortConfigurable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> sortExpression)
        {
            SortColumns.Add(new SortColumnDescriptor<TEntity>().Initialize(sortExpression, asc: false));
            return this;
        }

        public ISecondarySortConfigurable<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> sortExpression)
        {
            return OrderBy(sortExpression);
        }

        public ISecondarySortConfigurable<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> sortExpression)
        {
            return OrderByDescending(sortExpression);
        }
    }

    public interface ISortFieldMappingBuilder<TEntity>
    {
        IFirstSortConfigurable<TEntity> ForSortField(string sortField);
        IFirstSortConfigurable<TEntity> ForDtoSortField<TDto>(Expression<Func<TDto, object>> expr);
        bool CaseSensitive { get; set; }
    }

    public interface IFirstSortConfigurable<TEntity>
    {
        ISecondarySortConfigurable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> sortExpression);
        ISecondarySortConfigurable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> sortExpression);
    }

    public interface ISecondarySortConfigurable<TEntity>
    {
        ISecondarySortConfigurable<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> sortExpression);
        ISecondarySortConfigurable<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> sortExpression);
    }

}
