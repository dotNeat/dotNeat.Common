namespace UnitTest.dotNeat.Common.Patterns.ValueObjectPattern.Mocks
{
    using System.Collections.Generic;
    using global::dotNeat.Common.Patterns.ValueObjectPattern;

    public class Address
        : ValueObject
	{
        public String Street { get; private set; }
        public String City { get; private set; }
        public String State { get; private set; }
        public String Country { get; private set; }
        public String ZipCode { get; private set; }

        public Address(
            string street,
            string city,
            string state,
            string country,
            string zipcode
            )
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}

