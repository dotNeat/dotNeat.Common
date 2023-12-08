namespace dotNeat.Common.Patterns.EnumerationClassPattern
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class EnumerationBase<T> (int id, string name)
        : IEnumeration
        , IEquatable<T>
        , IComparable<EnumerationBase<T>>
        , IComparable
        where T : EnumerationBase<T>
    {
        public static T[] EnumMembers => 
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly
                                )
                .Select(f => f.GetValue(null))
                .Cast<T>()
                .ToArray();

        public static int EnumMembersCount => EnumMembers.Length;

        public string Name { get; } = name;

        public int Id { get; } = id;

        public override string ToString() => Name;


        public int CompareTo(EnumerationBase<T>? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not T otherValue)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        bool IEquatable<T>.Equals(T? other)
        {
            return this.Equals(other);
        }

        public int CompareTo(object? other) => Id.CompareTo(((other as T)!).Id);

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Id);
        }
    }
}

