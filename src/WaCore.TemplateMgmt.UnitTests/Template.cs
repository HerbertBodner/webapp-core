﻿using System;
using System.Collections.Generic;
using System.Text;
using WaCore.TemplateMgmt.Contracts.ValueObjects;

namespace WaCore.TemplateMgmt.UnitTests
{
    public class Template : ITemplate
    {
        public string Content { get; set; }
    }
}