using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NCApi.Common.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace NCApi.Common.Services
{
    public abstract class SqlSugarDBContext<T> : SqlSugarDBContext, ISqlSugarDBContext<T> where T : class, new()
    {
        public SqlSugarDBContext(ConnectionSetting connectionSetting) : base(connectionSetting) {}

        public int Count()
        {
            return base.DbClient.Queryable<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return base.DbClient.Queryable<T>().Count(expression);
        }

        public int Delete(T entity)
        {
            return base.DbClient.Deleteable<T>().ExecuteCommand();
        }

        public int Delete(List<dynamic> pkValue)
        {
            return base.DbClient.Deleteable<T>((List<object>)pkValue).ExecuteCommand();
        }

        public int Delete(List<T> entityList)
        {
            return base.DbClient.Deleteable<T>(entityList).ExecuteCommand();
        }

        public int Delete(Expression<Func<T, bool>> expression)
        {
            return base.DbClient.Deleteable<T>(expression).ExecuteCommand();
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return base.DbClient.Queryable<T>().Single(expression);
        }

        public IList<T> GetAll(Expression<Func<T, object>> orderByExpression = null, bool descending = true)
        {
            return base.DbClient.Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, descending ? SqlSugar.OrderByType.Desc : SqlSugar.OrderByType.Asc).ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> expression)
        {
            return base.DbClient.Queryable<T>().WhereIF(expression != null, expression).ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderByExpression, bool descending = true)
        {
            return base.DbClient.Queryable<T>().WhereIF(expression != null, expression).OrderByIF(orderByExpression != null, orderByExpression, descending ? SqlSugar.OrderByType.Desc : SqlSugar.OrderByType.Asc)
                .ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> expression, string orderFields, bool descending = true)
        {
            return base.DbClient.Queryable<T>().WhereIF(expression != null, expression).OrderByIF(!string.IsNullOrEmpty(orderFields), orderFields)
                .ToList();
        }

        public (int Total, IList<T> DataList) GetPageList(int page, int pageSize, Expression<Func<T, bool>> whereExpression = null, Expression<Func<T, object>> orderByExpression = null, bool descending = true)
        {
            int item = 0;
            List<T> item2 = base.DbClient.Queryable<T>()
                .WhereIF(whereExpression != null, whereExpression)
                .OrderByIF(orderByExpression != null, orderByExpression, descending ? SqlSugar.OrderByType.Desc : SqlSugar.OrderByType.Asc)
                .ToPageList(page - 1, pageSize, ref item);
            return (item, item2);
        }

        public (int Total, IList<T> DataList) GetPageList(int page, int pageSize, string whereString, string orderFields = null)
        {
            int item = 0;
            List<T> item2 = base.DbClient.Queryable<T>().WhereIF(!string.IsNullOrEmpty(whereString), whereString, (object)null).OrderByIF(!string.IsNullOrEmpty(orderFields), orderFields)
                .ToPageList(page - 1, pageSize, ref item);
            return (item, item2);
        }

        public void Save(T entity)
        {
            base.DbClient.Insertable<T>(entity).ExecuteCommandIdentityIntoEntity();
        }

        public void Save(List<T> entityList)
        {
            base.DbClient.Insertable<T>(entityList).ExecuteCommandIdentityIntoEntity();
        }

        public TResult Sum<TResult>(string sumField)
        {
            return base.DbClient.Queryable<T>().Sum<TResult>(sumField);
        }

        public TResult Sum<TResult>(Expression<Func<T, TResult>> expression)
        {
            return base.DbClient.Queryable<T>().Sum<TResult>(expression);
        }

        public int Update(T entity)
        {
            return base.DbClient.Updateable<T>(entity).ExecuteCommand();
        }

        public int Update(List<T> entityList)
        {
            return base.DbClient.Updateable<T>(entityList).ExecuteCommand();
        }

        public int Update(T entity, Expression<Func<T, object>> columns, Expression<Func<T, bool>> keyExpresstion)
        {
            return base.DbClient.Updateable<T>(entity).UpdateColumns(columns).Where(keyExpresstion)
                .ExecuteCommand();
        }
    }
}