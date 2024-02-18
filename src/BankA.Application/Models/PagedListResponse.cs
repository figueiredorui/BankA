using System;
using System.Collections.Generic;

namespace BankA.Application.Models
{
    public class PagedListResponse<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int PageCount { get; private set; }
        public int RecordCount { get; private set; }
        public List<T> Data { get; private set; }

        public PagedListResponse(List<T> items, int recordCount, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            RecordCount = recordCount;
            PageCount = (int)Math.Ceiling(RecordCount / (double)PageSize);

            Data = new List<T>();
            Data.AddRange(items);
        }
    }
}
