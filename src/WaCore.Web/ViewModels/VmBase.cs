using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Web.ViewModels
{
    public interface IVmBase
    {
        string DisplayName { get; set; }
        string DisplayDescription { get; set; }
    }

    public class VmBase : IVmBase
    {
        public string DisplayName { get; set; }

        public string DisplayDescription { get; set; }

        protected VmBase()
        { }

        protected VmBase(string displayName, string displayDescription)
        {
            DisplayName = displayName;
            DisplayDescription = displayDescription;
        }
    }
}
