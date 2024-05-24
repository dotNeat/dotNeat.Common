namespace UnitTest.dotNeat.Common.Patterns.EnumerationClassPattern.Mocks
{
    using global::dotNeat.Common.Patterns.EnumerationClassPattern;

    public class PropertyType
        : EnumerationBase<PropertyType>
    {
        public static PropertyType Apartment;
        public static PropertyType House;
        public static PropertyType Villa;
        public static PropertyType Farm;

        static PropertyType()
        {
            int enumValue = 0;
            PropertyType.Apartment = new(++enumValue, nameof(Apartment));
            PropertyType.House = new(++enumValue, nameof(House));
            PropertyType.Villa = new(++enumValue, nameof(Villa));
            PropertyType.Farm = new(++enumValue, nameof(Farm));
        }

        private PropertyType(int id, string name)
            : base(id, name)
        {
        }
    }
}

