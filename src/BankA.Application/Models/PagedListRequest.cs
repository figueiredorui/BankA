namespace BankA.Application.Models
{
    public class PagedListRequest
    {
        const int PAGESIZE = 25;

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public PagedListRequest()
        {
            this.PageIndex = 0;
            this.PageSize = PAGESIZE;
        }
        public PagedListRequest(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex < 0 ? 0 : pageIndex;
            this.PageSize = pageSize > PAGESIZE ? PAGESIZE : pageSize;
        }
    }
}
