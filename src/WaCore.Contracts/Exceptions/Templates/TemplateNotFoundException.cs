using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Contracts.Exceptions.Templates
{
    public class TemplateNotFoundException : Exception
    {
        public TemplateNotFoundException(string msg) : base(msg)
        {
        }
    }
}
