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

#if NETSTANDARD
            long remainder;
            long quotient = Math.DivRem(
                Convert.ToInt64(totalItems),
                Convert.ToInt64(itemsPerPage),
                out remainder
                );
            return Convert.ToUInt64( quotient + (remainder > 0 ? 1 : 0));
#else
            var result = Math.DivRem(totalItems, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1UL : 0UL);
#endif

        }

        public static long CalculateSkip(long pageNumber, long pageSize)
        {
            var result = (pageNumber - 1) * pageSize;
            return result;
        }

        public static long CalculateTotalPages(long totalItems, long itemsPerPage)
        {

#if NETSTANDARD
            long remainder;
            long quotient = Math.DivRem(
                totalItems,
                itemsPerPage,
                out remainder
                );
            return ( quotient + (remainder > 0 ? 1 : 0) );
#else
            var result = Math.DivRem(totalItems, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1L : 0L);
#endif

        }

        public static uint CalculateSkip(uint pageNumber, uint pageSize)
        {
            var result = (pageNumber - 1) * pageSize;
            return result;
        }

        public static uint CalculateTotalPages(uint totalItems, uint itemsPerPage)
        {

#if NETSTANDARD
            long remainder;
            long quotient = Math.DivRem(
                Convert.ToInt64(totalItems),
                Convert.ToInt64(itemsPerPage),
                out remainder
                );
            return Convert.ToUInt32(quotient + (remainder > 0 ? 1 : 0));
#else
            var result = Math.DivRem(totalItems, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1U : 0U);
#endif

        }

        public static int CalculateSkip(int pageNumber, int pageSize)
        {
            var result = (pageNumber - 1) * pageSize;
            return result;
        }

        public static int CalculateTotalPages(int totalItems, int itemsPerPage)
        {

#if NETSTANDARD
            int remainder;
            int quotient = Math.DivRem(
                totalItems,
                itemsPerPage,
                out remainder
                );
            return ( quotient + (remainder > 0 ? 1 : 0) );
#else
            var result = Math.DivRem(totalItems, itemsPerPage);
            return result.Quotient + (result.Remainder > 0 ? 1 : 0);
#endif

        }
    }
}
