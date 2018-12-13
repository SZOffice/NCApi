
using System.Linq;
using Microsoft.Extensions.Options;
using NCApi.Common.Domain;

namespace NCApi.Common.Services
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public abstract class ABTestingDBContext : DapperDBContext
    {
        private const string DB_CONFIG_NAME = "ABTestingDB";

        protected ABTestingDBContext(IOptions<ConnectionSettingList> options)
            : base(options.Value.ConnectionSettings.Where(c=>c.Name == DB_CONFIG_NAME).FirstOrDefault())
        {
        }
    }
}