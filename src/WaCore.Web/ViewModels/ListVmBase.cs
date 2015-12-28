using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Web.ViewModels
{
    public interface IVmList : IVmBase
    {
    }

    public class ListVmBase<TDto, TSearchConfig, TFilter> : VmBase, IVmList
        where TDto : class
        where TSearchConfig : SearchConfig<TFilter>, new()
        where TFilter : class
    {
        //Does not build for DNXCore
        //public StaticPagedList<TDto> DtoPagedList { get; set; }

        public TSearchConfig SearchConfig { get; set; }

        protected ListVmBase(string displayName, string displayDescription)
            : base(displayName, displayDescription)
        {
            SearchConfig = new TSearchConfig();
        }
    }
}
