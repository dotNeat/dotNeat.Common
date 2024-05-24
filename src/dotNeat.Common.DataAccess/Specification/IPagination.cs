namespace dotNeat.Common.DataAccess.Specification
{
    public interface IPagination
    {
        ulong PageNumber { get; }
        ulong PageSize { get; }
    }
}
