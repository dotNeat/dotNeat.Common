namespace UnitTest.dotNeat.Common.Patterns.ValueObjectPattern.Mocks
{
    using System;
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

