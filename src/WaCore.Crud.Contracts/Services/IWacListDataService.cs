﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Crud.Contracts.Services
{
    public interface IWacListDataService<TFilter, TDto>
    {
        List<TDto> GetAll(TFilter filter);
    }
}
