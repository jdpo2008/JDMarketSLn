using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, int totalCount)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.TotalCount = totalCount;
            this.IsSuccess = true;
            this.Message = null;
            this.Errors = null;
        }
    }
}
