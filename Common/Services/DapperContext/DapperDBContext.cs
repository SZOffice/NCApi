using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NCApi.Common.Domain;
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
    public abstract class DapperDBContext : IDapperDBContext
    {        
        private IDbTransaction _transaction;
        private int? _commandTimeout = null;
        private readonly ConnectionSetting ConnectionSetting;

        public IDbConnection Connection { get; private set; }
        public bool IsTransactionStarted { get; private set; }

        protected IDbConnection CreateConnection(ConnectionSetting connectionSetting)
        {
            var providerName = connectionSetting.ProviderName;
            if (providerName == "MySql.Data.MySqlClient") {
                return new MySqlConnection(connectionSetting.ConnectionString);
            }
            if (providerName == "System.Data.SqlClient") {
                return new SqlConnection(connectionSetting.ConnectionString);
            }
            
            throw new Exception("不支持的ProviderName: " + providerName);
        }

        protected DapperDBContext(ConnectionSetting connectionSetting)
        {
            ConnectionSetting = connectionSetting;

            Connection = CreateConnection(ConnectionSetting);
            Connection.Open();

            DebugPrint("Connection started.");
        }

        #region Transaction

        public void BeginTransaction()
        {
            if (IsTransactionStarted)
                throw new InvalidOperationException("Transaction is already started.");

            _transaction = Connection.BeginTransaction();
            IsTransactionStarted = true;

            DebugPrint("Transaction started.");
        }

        public void Commit()
        {
            if (!IsTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _transaction.Commit();
            _transaction = null;

            IsTransactionStarted = false;

            DebugPrint("Transaction committed.");
        }

        public void Rollback()
        {
            if (!IsTransactionStarted)
                throw new InvalidOperationException("No transaction started.");

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;

            IsTransactionStarted = false;

            DebugPrint("Transaction rollbacked and disposed.");
        }

        #endregion Transaction

        #region Dapper Execute & Query

        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return await Connection.ExecuteAsync(sql, param, _transaction, _commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return await Connection.QueryAsync<T>(sql, param, _transaction, _commandTimeout, commandType);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            return await Connection.QueryFirstOrDefaultAsync<T>(sql, param, _transaction, _commandTimeout, commandType);
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", CommandType commandType = CommandType.Text)
        {
            return await Connection.QueryAsync(sql, map, param, _transaction, true, splitOn, _commandTimeout, commandType);
        }

        #endregion Dapper Execute & Query

        public void Dispose()
        {
            if (IsTransactionStarted)
                Rollback();

            Connection.Close();
            Connection.Dispose();
            Connection = null;

            DebugPrint("Connection closed and disposed.");
        }

        private void DebugPrint(string message)
        {
#if DEBUG
            Debug.Print(">>> UnitOfWorkWithDapper - Thread {0}: {1}", Thread.CurrentThread.ManagedThreadId, message);
#endif
        }
    }
}
