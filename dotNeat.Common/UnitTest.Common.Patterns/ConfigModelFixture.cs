using System;
using System.Diagnostics;
using System.Drawing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTest.Common.Patterns.Structural.Composite.Mocks;

namespace UnitTest.Common.Patterns
{
    [TestClass]
    [TestCategory(nameof(ConfigModelFixture))]
    public class ConfigModelFixture
    {
        public enum SensorExtension
        {
            Location,
        }
        private static void Present(AppConfig appConfig, string title = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine(title);
            }
            Console.WriteLine("==================================");

            string configStringRendering = appConfig.RenderAsString();
            Console.WriteLine(configStringRendering);
            //Trace.WriteLine(configStringRendering);

            Console.WriteLine();
        }

        [TestMethod]
        public void BasicTest()
        {
            AppConfig appConfig = new AppConfig();

            Present(appConfig, "Defined Config");

            var configExtension = new ConfigValue<Point>(SensorExtension.Location, new Point(200, 300));
            appConfig.Add(configExtension);
 
            Present(appConfig, "Extended Config");
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
