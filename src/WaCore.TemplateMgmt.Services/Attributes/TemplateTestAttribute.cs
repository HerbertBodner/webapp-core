using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.TemplateMgmt.Services.Attributes
{
    public class TemplateTestAttribute : Attribute
    {
        public object Value { get; set; }
    }
}
