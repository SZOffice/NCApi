using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NCApi.Common.Services
{
    public interface ISqlSugarDBContext<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression);

        int Delete(T entity);

        int Delete(List<dynamic> pkValue);

        int Delete(List<T> entityList);

        int Delete(Expression<Func<T, bool>> expression);

        int Update(T entity);

        int Update(List<T> entityList);

        int Update(T entity, Expression<Func<T, object>> columns, Expression<Func<T, bool>> keyExpresstion);

        void Save(T entity);

        void Save(List<T> entityList);

        int Count();

        int Count(Expression<Func<T, bool>> expression);

        IList<T> GetAll(Expression<Func<T, object>> orderByExpression = null, bool descending = true);

        IList<T> GetList(Expression<Func<T, bool>> expression);

        IList<T> GetList(Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderByExpression, bool descending = true);

        IList<T> GetList(Expression<Func<T, bool>> expression, string orderFields, bool descending = true);

        (int Total, IList<T> DataList) GetPageList(int page, int pageSize, Expression<Func<T, bool>> whereExpression = null, Expression<Func<T, object>> orderByExpression = null, bool descending = true);

        (int Total, IList<T> DataList) GetPageList(int page, int pageSize, string whereString, string orderFields = null);

        TResult Sum<TResult>(string sumField);

        TResult Sum<TResult>(Expression<Func<T, TResult>> expression);
    }
}
