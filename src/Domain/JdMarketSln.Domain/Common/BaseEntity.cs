using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Domain.Common
{
    public abstract class BaseEntity : BaseAuditableEntity
    {
        public Guid Id { get; set; }
    }
}
