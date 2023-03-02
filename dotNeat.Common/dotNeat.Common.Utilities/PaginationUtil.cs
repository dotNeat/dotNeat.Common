namespace dotNeat.Common.Utilities
{
    using System;

    public static class PaginationUtil
    {
        public static ulong CalculateSkip(ulong pageNumber, ulong pageSize)
        {
            var result = (pageNumber - 1) * pageSize;
            return result;
        }

        public static ulong CalculateTotalPages(ulong totalItems, ulong itemsPerPage)
        {
            var result = Math.DivRem(totalItems, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1UL : 0UL);
        }

        public static long CalculateSkip(long pageNumber, long pageSize)
        {
            var result = (pageNumber - 1) * pageSize;
            return result;
        }

        public static long CalculateTotalPages(long totalItems, long itemsPerPage)
        {
            var result = Math.DivRem(totalItems, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1L : 0L);
        }

        public static uint CalculateSkip(uint pageNumber, uint pageSize)
        {
            var result = (pageNumber - 1) * pageSize;
            return result;
        }

        public static uint CalculateTotalPages(uint totalItems, uint itemsPerPage)
        {
            var result = Math.DivRem(totalItems, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1U : 0U);
        }

        public static int CalculateSkip(int pageNumber, int pageSize)
        {
            var result = (pageNumber - 1) * pageSize;
            return result;
        }

        public static int CalculateTotalPages(int totalItems, int itemsPerPage)
        {
            var result = Math.DivRem(totalItems, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1 : 0);
        }
    }
}
