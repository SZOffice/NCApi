using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCApi.Domain;
using NCApi.Common.Services;
using NCApi.Common.Domain;

namespace NCApi.Repositories
{
    public interface ISqlSugarABTestingRepository : ISqlSugarDBContext<SqlSugarABTesting>
    {
        PageList<SqlSugarABTesting> GetPageList(ABTestingPageRequest request);
    }
}
