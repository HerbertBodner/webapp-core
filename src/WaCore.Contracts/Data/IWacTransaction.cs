using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Contracts.Data
{
    public interface IWacTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
