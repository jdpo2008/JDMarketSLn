using JDMarketSLn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Domain.Entities
{
    public class UnitMeasureProduct : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
    }
}
