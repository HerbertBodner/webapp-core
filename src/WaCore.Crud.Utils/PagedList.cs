using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WaCore.Crud.Contracts.Utils;

namespace WaCore.Crud.Utils
{
    public class PagedList<T> : IPagedList<T>
    {
        private IList<T> _list;

        public int TotalCount { get; }

        public int Offset { get; }

        public int? Limit { get; }

        public IList<T> List => _list;

        
        public PagedList(IList<T> list, int totalCount, int offset, int? limit)
        {
            _list = new List<T>(list);
            TotalCount = totalCount;
            Offset = offset;
            Limit = limit;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
