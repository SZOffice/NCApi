using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NCApi.Common.Services
{
    public interface IDapperDBContext : IDisposable
    {
        IDbConnection Connection { get; }

        bool IsTransactionStarted { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}
