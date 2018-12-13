
using System.Collections.Generic;

namespace NCApi.Common.Domain
{
    public class ConnectionSettingList
    {
        public ConnectionSetting[] ConnectionSettings { get; set; }
    }

    public class ConnectionSetting
    {
        public int CommandTimeout
        {
            get;
            set;
        } = 30;


        public string ConnectionString
        {
            get;
            set;
        }

        public bool IsReadonlyDB
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string ProviderName
        {
            get;
            set;
        }
    }
}