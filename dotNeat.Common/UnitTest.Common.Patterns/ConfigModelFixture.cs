using System;
using System.Diagnostics;
using System.Drawing;

using dotNeat.Common.Patterns;
using dotNeat.Common.Patterns.Structural.Composite;

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

        private static void PresentComponentsIDs(IComponent component, string indentation = "")
        {
            IEntity entity = component as IEntity;
            if(entity == null)
            {
                return;
            }

            indentation += "  ";
            string id = indentation + entity.ID;
            Console.WriteLine(id);
            Trace.WriteLine(id);

            foreach(var child in component.GetComponents())
            {
                PresentComponentsIDs(child, indentation);
            }
        }

        [TestMethod]
        public void BasicTest()
        {
            AppConfig appConfig = new AppConfig();

            Present(appConfig, "Defined Config");
            Console.WriteLine("Structure:");
            Trace.WriteLine("Structure");
            PresentComponentsIDs(appConfig);
            Console.WriteLine();
            Trace.WriteLine(string.Empty);

            var configExtension = new ConfigValue<Point>(SensorExtension.Location, new Point(200, 300));
            appConfig.Add(configExtension);
 
            Present(appConfig, "Extended Config");
            Console.WriteLine("Structure:");
            Trace.WriteLine("Structure");
            PresentComponentsIDs(appConfig);
            Console.WriteLine();
            Trace.WriteLine(string.Empty);
        }

        [TestMethod]
        public void ShowChildComponentIDs()
        {
            AppConfig appConfig = new AppConfig();

            foreach(var id in appConfig.GetComponentIDs())
            {
                Console.WriteLine(id);
                Trace.WriteLine(id);
            }
        }

        [TestMethod]
        public void ShowComponentDefinitionStructure()
        {
            AppConfig appConfig = new AppConfig();

            PresentComponentsIDs(appConfig);
        }
    }
}
