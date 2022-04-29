using JdMarketSln.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JdMarketSln.Domain.Entities
{
    public class Suplier : BaseEntity
    {
        public string BusinessName { get; set; }
        public string TaxIdentifier { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adrress { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }
}
