namespace dotNeat.Common.Utilities
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Linq;

    public static class EnumUtil
    {
        public static TEnum[] GetValues<TEnum>()
            where TEnum : struct, Enum
        {
#if NETSTANDARD
            TEnum[] enumMembers = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
#else
            TEnum[] enumMembers = Enum.GetValues<TEnum>();
#endif
            return enumMembers;
        }

        public static TEnum[] GetEnumMembersList<TEnum>()
            where TEnum : struct, Enum
        {
            TEnum[] enumMembers = EnumUtil.GetValues<TEnum>();
            return enumMembers;
        }

        public static TEnum[] GetEmptyMembersList<TEnum>()
            where TEnum : struct, Enum
        {
            TEnum[] enumMembers = Array.Empty<TEnum>();
            return enumMembers;
        }

        public static string GetDescription<TEnum>(TEnum value)
            where TEnum : struct, Enum
        {
            DescriptionAttribute? descriptionAttribute = ReflectionUtil.GetAttribute<DescriptionAttribute>(value);
            string description = descriptionAttribute is not null ? descriptionAttribute.Description : value.ToString();
            return description;
        }

        public static string GetCachedDescription<TEnum>(TEnum enumMember)
            where TEnum : struct, Enum
        {
            Type enumType = typeof(TEnum);
            if (enumDescriptionsCache.TryGetValue(enumType, out var enumDescrioptions))
            {
                if (enumDescrioptions.TryGetValue(enumMember, out var enumDescription))
                {
                    return enumDescription;
                }
                else
                {
                    string description = GetDescription(enumMember);
                    enumDescrioptions.GetOrAdd(enumMember, description);
                    return description;
                }
            }
            else
            {
                ConcurrentDictionary<Enum, string> newEnumDescrioptions =
                    enumDescriptionsCache.GetOrAdd(
                        enumType,
                        new ConcurrentDictionary<Enum, string>(10, EnumUtil.GetValues<TEnum>().Length)
                        );

                string descr = newEnumDescrioptions.GetOrAdd(enumMember, GetDescription(enumMember));
                return descr;
            }
        }

        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Enum, string>> enumDescriptionsCache = new();
    }
}

