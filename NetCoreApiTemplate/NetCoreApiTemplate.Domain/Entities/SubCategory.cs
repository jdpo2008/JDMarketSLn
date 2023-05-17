using JDMarketSLn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Domain.Entities
{
    public class SubCategory : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }


        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
