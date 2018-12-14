
using Dapper.Contrib.Extensions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCApi.Domain
{
    [SugarTable("abtesting_jdb")]
    public class SqlSugarABTesting
    {
        [SugarColumn(ColumnName = "id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "feature_type")]
        public EmpABTestingFeatureType FeatureType { get; set; }

        [SugarColumn(ColumnName = "ab_result")]
        public byte ABResult { get; set; }

        [SugarColumn(ColumnName = "created_time")]
        public DateTime CreatedTime { get; set; }

        [SugarColumn(ColumnName = "updated_time")]
        public DateTime UpdatedTime { get; set; }

    }
}
