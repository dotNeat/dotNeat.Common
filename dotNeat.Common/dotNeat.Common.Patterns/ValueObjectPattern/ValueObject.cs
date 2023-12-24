namespace dotNeat.Common.Patterns.ValueObjectPattern
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using Utilities;
    using Utilities.Diagnostics;

    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject? left, ValueObject? right)
        {
            if (ReferenceEquals(left, right))
            {
                // the very same object
                return true;
            }

            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                // one null another non-null:
                return false;
            }

            return ReferenceEquals(left, right) || left!.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        private object[] GetEqualityComponents()
        {
            object[] components = DoGetEqualityComponents();
            AssertConcreteImplementationInvariants(components);
            return components;
        }
        protected abstract object[] DoGetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            { 
                return false;
            }

            var other = (ValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject one, ValueObject two)
        {
            return EqualOperator(one, two);
        }

        public static bool operator !=(ValueObject one, ValueObject two)
        {
            return NotEqualOperator(one, two);
        }

        [Conditional("DEBUG")]
        private void AssertConcreteImplementationInvariants(object[] equalityComponents)
        {
            PropertyInfo[] propertyInfos = ReflectionUtil.GetAllPublicInstanceProperties(this.GetType());

            if (propertyInfos.Length != equalityComponents.Length)
                throw new TypeImplementationException(
                    $"All the public properties are expected to be returned by {nameof(this.DoGetEqualityComponents)} method."
                    );

            if (propertyInfos
                    .Select(x => x.GetValue(this)?.GetHashCode() ?? 0)
                    .Aggregate((x, y) => x ^ y)
                != equalityComponents
                    .Select(x => x.GetHashCode())
                    .Aggregate((x, y) => x ^ y)
                )
                throw new TypeImplementationException(
                    $"The HashCode calculation based on public property values reflection is expected to match to the {nameof(ValueObject)}'s {this.GetHashCode()} method call result."
                    );

            //Debug.Assert(
            //    propertyInfos.Length == this.GetEqualityComponents().Count(),
            //    $"All the public properties are expected to be returned by {this.GetEqualityComponents()} method."
            //);

            //Debug.Assert(
            //    propertyInfos.Select(x => x.GetValue(this)?.GetHashCode() ?? 0).Aggregate((x, y) => x ^ y) == this.GetHashCode(),
            //    $"The HashCode calculation based on public property values reflection is expected to match to the {nameof(ValueObject)}'s {this.GetHashCode()} method call result."
            //    );

        }
    }
}

