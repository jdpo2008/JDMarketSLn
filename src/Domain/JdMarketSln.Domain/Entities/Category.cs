using JdMarketSln.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JdMarketSln.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
