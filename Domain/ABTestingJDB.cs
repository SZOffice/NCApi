using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCApi.Domain
{
    [Table("abtesting_jdb")]
    public class ABTestingJDB
    {
        [Key]
        public int Id { get; set; }

        public int User_Id { get; set; }

        public EmpABTestingFeatureType Feature_Type { get; set; }

        public byte AB_Result { get; set; }

        public DateTime Created_Time { get; set; }

        public DateTime Updated_Time { get; set; }

    }
}
