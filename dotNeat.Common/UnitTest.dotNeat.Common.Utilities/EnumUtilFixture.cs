using dotNeat.Common.Utilities;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using cm = System.ComponentModel;

namespace Test.Dag.Common.Utilities
{
    [TestClass()]
    public class EnumUtilFixture
    {
        [TestMethod()]
        public void GetCachedDescriptionTest()
        {
            Assert.AreEqual("Member 1", EnumUtil.GetCachedDescription(Enumo1.Member1));
            Assert.AreEqual(Enumo1.Member2.ToString(), EnumUtil.GetCachedDescription(Enumo1.Member2));
            Assert.AreEqual(Enumo1.Member3.ToString(), EnumUtil.GetCachedDescription(Enumo1.Member3));
        }
    }

    #region mocks


    public enum Enumo1
    {
        [cm.Description("Member 1")]
        Member1,

        Member2,

        Member3,
    }

    #endregion mocks
}