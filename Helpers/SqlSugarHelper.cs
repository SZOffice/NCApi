using System;
using SqlSugar;
using NCApi.Common.Domain;

namespace NCApi.Helpers
{    
    public static class SqlSugarHelper
    {
        public static SqlSugarClient GetSqlSugarClient(ConnectionSetting setting)
        {
            if (setting != null)
            {
                ConnectionConfig connConfig = new ConnectionConfig() {
                    ConnectionString = setting.ConnectionString,
                    DbType = GetDbType(setting.ProviderName),
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true
                };
                var db = new SqlSugarClient(connConfig);
                db.Ado.CommandTimeOut = setting.CommandTimeout;
                return db;
            }
            return null;
        }

        private static SqlSugar.DbType GetDbType(string providerName)
        {
            string text = providerName.ToLower();
            if (text.Contains("mysqlclient"))
            {
                return SqlSugar.DbType.MySql;
            }
            if (text.Contains("sqlclient"))
            {
                return SqlSugar.DbType.SqlServer;
            }
            if (text.Contains("sqliteclient"))
            {
                return SqlSugar.DbType.Sqlite;
            }
            if (text.Contains("oracleclient"))
            {
                return SqlSugar.DbType.Oracle;
            }
            throw new Exception("不支持的ProviderName: " + providerName);
        }
    }

}