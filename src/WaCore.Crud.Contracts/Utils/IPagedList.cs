using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Crud.Contracts.Utils
{
    public interface IPagedList<T> : IEnumerable<T>
    {
        IList<T> List { get; }
        
        int TotalCount { get; }
        int Offset { get; }
        int? Limit { get; }
    }
}
