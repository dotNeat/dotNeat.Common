using System;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTest.Common.Patterns.Structural.Composite.Mocks;

namespace UnitTest.Common.Patterns
{
    [TestClass]
    [TestCategory(nameof(ConfigModelFixture))]
    public class ConfigModelFixture
    {
        [TestMethod]
        public void BasicTest()
        {
            AppConfig appConfig = new AppConfig();

            string configStringRendering = appConfig.RenderAsString();
            Console.WriteLine(configStringRendering);
            Trace.WriteLine(configStringRendering);
        }

        [TestMethod]
        public void ShowComponentIDs()
        {
            AppConfig appConfig = new AppConfig();

            foreach(var id in appConfig.GetComponentIDs())
            {
                Console.WriteLine(id);
                Trace.WriteLine(id);
            }
        }
    }
}
