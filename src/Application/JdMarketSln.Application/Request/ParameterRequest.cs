using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Request
{
    public class ParameterRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ParameterRequest()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public ParameterRequest(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
