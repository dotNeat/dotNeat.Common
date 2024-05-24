namespace UnitTest.dotNeat.Common.Patterns.ValueObjectPattern.Mocks
{
    using System;
    using global::dotNeat.Common.Patterns.ValueObjectPattern;

    public class InvalidAddressImplementation(
        string street,
        string city,
        string state,
        string country,
        string zipcode)
        : ValueObject
    {
        public String Street { get; private set; } = street;
        public String City { get; private set; } = city;
        public String State { get; private set; } = state;
        public String Country { get; private set; } = country;
        public String ZipCode { get; private set; } = zipcode;

        protected override object[] DoGetEqualityComponents()
        {
            return [
                Street,
                City,
                //State, //NOTE: we intentionally "forgot" to include one public property of this value object
                Country,
                ZipCode
            ];
        }
    }
}

