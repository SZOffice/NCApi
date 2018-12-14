using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using NCApi.Common.Domain;
using NCApi.Common.Services;
using NCApi.Domain;

namespace NCApi.Repositories.Impl
{
    public class SqlSugarABTestingRepository : AWSSugarDBContext<SqlSugarABTesting>, ISqlSugarABTestingRepository
    {
        public SqlSugarABTestingRepository(IOptions<ConnectionSettingList> options) : base(options) {}

        public PageList<SqlSugarABTesting> GetPageList(ABTestingPageRequest request)
        {
            var query = DbClient.Queryable<SqlSugarABTesting>()
                                .Where(c => c.Id >= request.Id)
                                //.WhereIF(!string.IsNullOrEmpty(request.MemberId), (c, cc) => cc.MemberId == request.MemberId)
                                .GroupBy(c => new {
                                    c.Id

                                });

            int total = query.Count();

            var resultList = query.OrderBy(c => c.Id, SqlSugar.OrderByType.Desc)
                                .Skip((request.Page - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToList();
 
            var pageList = new PageList<SqlSugarABTesting>() {
                Page = request.Page,
                PageSize = request.PageSize,
                Rows = resultList,
                Total = total
            };

            return pageList;
        }
    }
}
