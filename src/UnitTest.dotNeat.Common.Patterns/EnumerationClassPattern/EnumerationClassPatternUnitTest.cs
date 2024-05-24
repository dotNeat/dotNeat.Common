using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.dotNeat.Common.Patterns.EnumerationClassPattern.Mocks;

namespace UnitTest.dotNeat.Common.Patterns.EnumerationClassPattern
{
    [TestClass]
    public class EnumerationClassPatternUnitTest
	{
        [TestMethod]
        public void TestEnumerationClass()
        {
            Assert.AreEqual(3, CardType.EnumMembersCount);

            Assert.AreEqual(4, PropertyType.EnumMembersCount);
        }
    }
}

