

namespace UnitTest.dotNeat.Common.Patterns.ValueObjectPattern
{
    using System.Diagnostics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mocks;
    using global::dotNeat.Common.Utilities.Diagnostics;

    [TestClass]
    public class ValueObjectPatternUnitTest
	{
        [TestMethod]
        public void TestValueObject()
        {
            Address? address1 = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");
            Address? address2 = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");
            Address? address3 = address1;
            Address? address4 = new Address("12 Microsoft Way", "Redmond", "WA", "US", "98052");
            Address? address5 = null;

            Assert.IsNotNull(address1);
            Assert.IsNotNull(address2);
            Assert.IsNotNull(address3);
            Assert.IsNotNull(address4);
            Assert.IsNull(address5);

            Assert.IsTrue(address1 == address2);
            Assert.IsTrue(address2 == address3);
            Assert.IsTrue(address3 == address1);
            Assert.IsTrue(address3.Equals(address1));
            Assert.IsTrue(address3.Equals(address2));
            Assert.IsTrue(object.ReferenceEquals(address1, address3));
            Assert.IsFalse(object.ReferenceEquals(address1, address2));
            Assert.IsFalse(object.ReferenceEquals(address4, address1));
            Assert.IsFalse(object.ReferenceEquals(address5, address1));
            Assert.IsTrue(object.ReferenceEquals(address5, null));
        }

        [TestMethod]
        [Conditional("DEBUG")]
        public void TestInvalidValueObjectImplementation()
        {
            var address1 = new InvalidAddressImplementation("1 Microsoft Way", "Redmond", "WA", "US", "98052");
            Assert.ThrowsException<TypeImplementationException>(() => address1.GetHashCode());
            //Assert.ThrowsException<TypeImplementationException>(() => address1.Equals(null));

            var address2 = new InvalidAddressImplementation("2 Microsoft Way", "Redmond", "WA", "US", "98052");
            Assert.ThrowsException<TypeImplementationException>(() => address1.Equals(address2));
        }
    }
}

