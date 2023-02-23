namespace dotNeat.Common.DataAccess.Specification
{
    public class Pagination
        : IPagination
    {
        public Pagination(ulong pageNumber, ulong pageSize) 
        {
            PageNumber = pageNumber;
            PageSize = pageSize;    
        }
        public ulong PageNumber { get; set; }
        public ulong PageSize { get; }
    }
}
