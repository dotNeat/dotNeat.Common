namespace UnitTest.dotNeat.Common.Patterns.ValueObjectPattern
{
    using System;
    using System.Diagnostics;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using global::dotNeat.Common.Patterns.ValueObjectPattern;
    using UnitTest.dotNeat.Common.Patterns.ValueObjectPattern.Mocks;

    [TestClass]
    public class ValueObjectPatternUnitTest
	{
		public ValueObjectPatternUnitTest()
		{
		}

        [TestMethod]
        public void TestValueObject()
        {
            var address1 = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");
            var address2 = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");
            var address3 = address1;
            var address4 = new Address("12 Microsoft Way", "Redmond", "WA", "US", "98052");
            Address address5 = null;

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
            Assert.IsFalse(address4 == address1);
            Assert.IsFalse(address5 == address1);
            Assert.IsTrue(address5 == null);


        }
    }
}

