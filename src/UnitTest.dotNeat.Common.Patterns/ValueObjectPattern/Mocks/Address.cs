namespace UnitTest.dotNeat.Common.Patterns.ValueObjectPattern.Mocks
{
    using System;
    using System.Collections.Generic;
    using global::dotNeat.Common.Patterns.ValueObjectPattern;

    public class Address
        : ValueObject
	{
        public String Street { get; }
        public String City { get; }
        public String State { get; }
        public String Country { get; }
        public String ZipCode { get; }

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

        protected override object[] DoGetEqualityComponents()
        {
            return [
                Street, 
                City, 
                State, 
                Country, 
                ZipCode
            ]; 
        }
    }
}

