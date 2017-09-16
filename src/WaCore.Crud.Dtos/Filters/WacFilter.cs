using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Dtos.Filters
{
    public class WacFilter : IWacFilter
    {
        protected static Dictionary<string, string> sortingFieldMappingDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public int Offset { get; set; }

        public int? Limit { get; set; }

        public string SortBy { get; set; }


        public WacFilter()
        {
            Offset = 0;
        }

        public virtual string GetDbSortField(string dtoField)
        {
            return sortingFieldMappingDict.ContainsKey(dtoField) ? sortingFieldMappingDict[dtoField] : null;
        }
    }
}
