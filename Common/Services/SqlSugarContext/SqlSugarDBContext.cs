using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NCApi.Common.Domain;
using NCApi.Helpers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NCApi.Common.Services
{
    public class SqlSugarDBContext : IDisposable
    {
        private bool disposed;

        public SqlSugarClient DbClient
        {
            get;
            set;
        }

        public SqlSugarDBContext(ConnectionSetting connectionSetting)
        {
            DbClient = SqlSugarHelper.GetSqlSugarClient(connectionSetting);
        }
        
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
            }
            disposed = true;
        }
    }
}