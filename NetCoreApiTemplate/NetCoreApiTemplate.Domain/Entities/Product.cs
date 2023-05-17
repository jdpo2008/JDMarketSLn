using JDMarketSLn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid ProductDetailId { get; set; }

        public virtual ProductDetail ProductDetail { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }

    }
}
