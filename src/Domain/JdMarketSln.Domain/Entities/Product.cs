using JdMarketSln.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SuplierId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Suplier Suplier { get; set; }
        
    }
}
