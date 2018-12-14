
using System.Linq;
using Microsoft.Extensions.Options;
using NCApi.Common.Domain;

namespace NCApi.Common.Services
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public abstract class AWSSugarDBContext<T> : SqlSugarDBContext<T>
        where T : class, new()
    {
        private const string DB_CONFIG_NAME = "AWSDB";

        protected AWSSugarDBContext(IOptions<ConnectionSettingList> options)
            : base(options.Value.ConnectionSettings.Where(c=>c.Name == DB_CONFIG_NAME).FirstOrDefault())
        {
        }
    }
}