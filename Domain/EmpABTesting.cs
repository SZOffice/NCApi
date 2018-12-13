using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCApi.Domain
{
    [Table("EmpABTesting")]
    public class EmpABTesting
    {
        [Key]
        public int Id { get; set; }

        public int HirerId { get; set; }

        public EmpABTestingFeatureType FeatureType { get; set; }

        public byte ABValue { get; set; }

        public DateTime utcCreatedTime { get; set; }

        public DateTime utcLastModifiedTime { get; set; }

    }
}
