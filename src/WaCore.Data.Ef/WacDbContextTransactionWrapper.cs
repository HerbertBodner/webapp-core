using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using WaCore.Contracts.Data;

namespace WaCore.Data.Ef
{
    public class WacDbContextTransactionWrapper : IWacTransaction
    {
        private IDbContextTransaction _dbContextTransaction;

        public WacDbContextTransactionWrapper(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public void Commit()
        {
            _dbContextTransaction.Commit();
        }

        public void Dispose()
        {
            _dbContextTransaction.Dispose();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }
    }
}
