using NetCoreApiTemplate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Domain.Entities
{
    public class Log : BaseEntity
    {
        public DateTime When { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public string Exception { get; set; }
        public string Trace { get; set; }
        public string Logger { get; set; }

    }
}
