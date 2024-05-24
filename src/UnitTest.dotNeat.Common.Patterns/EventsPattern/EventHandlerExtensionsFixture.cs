using dotNeat.Common.Patterns.EventsPattern;

namespace UnitTest.dotNeat.Common.Patterns.EventsPattern
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class EventHandlerExtensionsFixture
    {
        private bool _methodExecuted;
        private event EventHandler? TestEvent;
        private event EventHandler<EventArgs>? TestEventT;

        private void testMethod(object? sender, EventArgs e)
        {
            _methodExecuted = true;
        }

        [TestInitialize]
        public void FixtureInit()
        {
            TestEvent += testMethod;
            TestEventT += testMethod;
            _methodExecuted = false;
        }

        [TestCleanup]
        public void FixtureDispose()
        {
            TestEvent -= new(testMethod);
            TestEventT -= new(testMethod);
        }

        [TestMethod]
        [TestCategory("Common.Extensions")]
        public void RaiseEventOfT()
        {
            TestEventT?.Raise(this, EventArgs.Empty);
            Assert.IsTrue(_methodExecuted, "The test method was not executed");
        }

        [TestMethod]
        [TestCategory("Common.Extensions")]
        public void RaiseEvent()
        {
            TestEvent?.Raise(this, EventArgs.Empty);
            Assert.IsTrue(_methodExecuted, "The test method was not executed");
        }
    }
}