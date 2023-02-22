namespace dotNeat.Common.Utilities
{
    using System;

    public static class EnumFlagsUtil
    {

        public static TFlags[] GetAllFlagsList<TFlags>()
            where TFlags : struct, Enum
        {
            return EnumUtil.GetEnumMembersList<TFlags>();
        }

        public static TFlags[] GetEmptyFlagsList<TFlags>()
            where TFlags : struct, Enum
        {
            return EnumUtil.GetEmptyMembersList<TFlags>();
        }

        //public static TFlags? GetFlagsComposition<TFlags>(IEnumerable<TFlags> individualFlags)
        //    where TFlags : struct, Enum
        //{
        //    throw new NotImplementedException();
        //}

        //public static TFlags GetAllFlagsComposition<TFlags>()
        //    where TFlags : struct, Enum
        //{
        //    throw new NotImplementedException();
        //}
    }
}

