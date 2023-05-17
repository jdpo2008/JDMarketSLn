using JDMarketSLn.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Domain.Entities
{
    public class ProductDetail : BaseAuditableEntity
    {
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public decimal Price { get; set; }
        public Int64 Stock { get; set; }
        public Int64 StockMin { get; set; }
        public Int64 StockMax { get; set; }
        public Guid UnitMeasureProductId { get; set; }
        public DateTime? DueDate { get; set; }                             
        public bool? Deprecated { get; set; }

        public virtual UnitMeasureProduct UnitMeasureProduct { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
