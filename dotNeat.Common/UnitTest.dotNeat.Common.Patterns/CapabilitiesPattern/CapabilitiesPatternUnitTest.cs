namespace UnitTest.dotNeat.Common.Patterns.CapabilitiesPattern
{
	using System;
    using System.Diagnostics;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using dotNeat.Common.Patterns.CapabilitiesPattern;
    using global::dotNeat.Common.Patterns.CapabilitiesPattern;
    using global::dotNeat.Common.Patterns.EventsPattern;

    [TestClass]
    public class CapabilitiesPatternUnitTest
	{
		public CapabilitiesPatternUnitTest()
		{
		}

        [TestMethod]
        public void TestCapabilitiesPattern()
        {
            //Constructing root entity
            Trace.WriteLine("*** Constructing root entity");
            Entity compositeEntityRoot = new StatusCapableEntity();
            compositeEntityRoot.TraceEntityCapabilities();
            Assert.AreEqual(1, compositeEntityRoot.GetAllImplementedCapabilityTypes().Length);
            Assert.AreEqual(1, compositeEntityRoot.GetAllCapabilityImplementations().Length);
            Assert.AreEqual(1, compositeEntityRoot.GetImplementationsOf<IStatusReportCapable>().Length);
            Assert.AreSame(compositeEntityRoot, compositeEntityRoot.GetImplementationsOf<IStatusReportCapable>()[0]);
            Assert.AreEqual(0, compositeEntityRoot.GetImplementationsOf<IPttCapable>().Length);

            //Constructing child entity
            Trace.WriteLine("*** Constructing child entity");
            Entity pttChildEntity = new PttCapableEntity();
            pttChildEntity.TraceEntityCapabilities();
            Assert.AreEqual(1, pttChildEntity.GetAllImplementedCapabilityTypes().Length);
            Assert.AreEqual(1, pttChildEntity.GetAllCapabilityImplementations().Length);
            Assert.AreEqual(1, pttChildEntity.GetImplementationsOf<IPttCapable>().Length);
            Assert.AreSame(pttChildEntity, pttChildEntity.GetImplementationsOf<IPttCapable>()[0]);
            Assert.AreEqual(0, pttChildEntity.GetImplementationsOf<IStatusReportCapable>().Length);

            //Extending composite entity via child (IPttCapable) entity
            Trace.WriteLine("*** Extending composite entity via child (IPttCapable) entity");
            compositeEntityRoot.Add(pttChildEntity);
            compositeEntityRoot.TraceEntityCapabilities();
            Assert.AreEqual(2, compositeEntityRoot.GetAllImplementedCapabilityTypes().Length);
            Assert.AreEqual(2, compositeEntityRoot.GetAllCapabilityImplementations().Length);
            Assert.AreEqual(1, compositeEntityRoot.GetImplementationsOf<IStatusReportCapable>().Length);
            Assert.AreEqual(1, compositeEntityRoot.GetImplementationsOf<IPttCapable>().Length);
            Assert.AreSame(compositeEntityRoot, compositeEntityRoot.GetImplementationsOf<IStatusReportCapable>()[0]);
            Assert.AreSame(pttChildEntity, compositeEntityRoot.GetImplementationsOf<IPttCapable>()[0]);

            //Discover the PTT capability and get reference to its implementation:
            IPttCapable[] pttImplementations = compositeEntityRoot.GetImplementationsOf<IPttCapable>();
            IPttCapable ptt = pttImplementations.Length > 0 ? pttImplementations[0] : null;
            Assert.AreSame(pttChildEntity, ptt);

            //Remove the child (IPttCapable) entity
            Trace.WriteLine("*** Remove the child (IPttCapable) entity");
            compositeEntityRoot.Remove(pttChildEntity);
            compositeEntityRoot.TraceEntityCapabilities();
            Assert.AreEqual(1, compositeEntityRoot.GetAllImplementedCapabilityTypes().Length);
            Assert.AreEqual(1, compositeEntityRoot.GetAllCapabilityImplementations().Length);
            Assert.AreEqual(1, compositeEntityRoot.GetImplementationsOf<IStatusReportCapable>().Length);
            Assert.AreSame(compositeEntityRoot, compositeEntityRoot.GetImplementationsOf<IStatusReportCapable>()[0]);
            Assert.AreEqual(0, compositeEntityRoot.GetImplementationsOf<IPttCapable>().Length);

            //More complex composition:
            compositeEntityRoot.Add(new ThisCapableEntity());
            compositeEntityRoot.Add(new ThatCalableEntity());
            compositeEntityRoot.Add(new ThisAndThatCapableEntity());
            compositeEntityRoot.TraceEntityCapabilities();
            Assert.AreEqual(3, compositeEntityRoot.GetAllImplementedCapabilityTypes().Length);
            Assert.AreEqual(5, compositeEntityRoot.GetAllCapabilityImplementations().Length);

        }

    }

    #region test mocks

    internal interface IPttCapable : ICapability
    {
        void StartPtt();
        void StopPtt();
        bool IsPttActive { get; }
    }

    internal interface IStatusReportCapable : ICapability
    {
        int Status { get; }
        event EventHandler<DataChangeEventArgs<int>> StatusChanged;
    }

    internal interface IThisCapability : ICapability
    {
        void DoThis();
    }

    internal interface IThatCapability : ICapability
    {
        void DoThat();
    }

    internal class Entity : CompositeCapabilitiesHost
    {
        internal void TraceEntityCapabilities()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.GetType().Name + ":");
            sb.AppendLine("   Supported capability types:");
            foreach (var i in this.GetAllImplementedCapabilityTypes())
                sb.AppendLine("      " + i.Name);
            sb.AppendLine("   All capability implementations:");
            foreach (var i in this.GetAllCapabilityImplementations())
                sb.AppendLine("      " + i.GetType().Name);
            sb.AppendLine();
            Trace.WriteLine(sb);
        }
    }

    internal class PttCapableEntity : Entity, IPttCapable
    {
        private bool _isPttActive = false;

        public void StartPtt()
        {
            _isPttActive = true;
            Trace.WriteLine("Start PTT...");
        }

        public void StopPtt()
        {
            _isPttActive = false;
            Trace.WriteLine("Stop PTT.");
        }

        public bool IsPttActive
        {
            get { return _isPttActive; }
        }

        public virtual bool IsPttAvailable
        {
            get { return _isPttAvailable; }
            set
            {
                if (_isPttAvailable == value)
                    return;
                DataChangeEventArgs<bool> eArgs =
                    new DataChangeEventArgs<bool>(_isPttAvailable, value);
                OnIsPttAvailableChanging(eArgs);
                _isPttAvailable = value;
                OnIsPttAvailableChanged(eArgs);
            }
        }
        private bool _isPttAvailable = false;

        protected virtual void OnIsPttAvailableChanging(DataChangeEventArgs<bool> eArgs)
        {
            this.RaiseDataChangeEventHandler(IsPttAvailableChanging, eArgs);
        }

        protected virtual void OnIsPttAvailableChanged(DataChangeEventArgs<bool> eArgs)
        {
            this.RaiseDataChangeEventHandler(IsPttAvailableChanged, eArgs);
        }

        public event EventHandler<DataChangeEventArgs<bool>> IsPttAvailableChanging;

        public event EventHandler<DataChangeEventArgs<bool>> IsPttAvailableChanged;


        void IPttCapable.StartPtt()
        {
            this.StartPtt();
        }

        void IPttCapable.StopPtt()
        {
            this.StopPtt();
        }

        bool IPttCapable.IsPttActive
        {
            get { return this.IsPttActive; }
        }

        bool ICapability.IsAvailable
        {
            get { throw new NotImplementedException(); }
        }

        event EventHandler<DataChangeEventArgs<bool>> ICapability.IsAvailableChanging
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event EventHandler<DataChangeEventArgs<bool>> ICapability.IsAvailableChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }

    internal class StatusCapableEntity : Entity, IStatusReportCapable
    {
        private int _status = int.MaxValue;
        public int Status
        {
            get { return _status; }
            set
            {
                if (_status == value)
                    return;
                DataChangeEventArgs<int> eArgs = new DataChangeEventArgs<int>(_status, value);
                _status = value;
                OnStatusChanged(eArgs);
            }
        }

        public event EventHandler<DataChangeEventArgs<int>> StatusChanged;

        protected virtual void OnStatusChanged(DataChangeEventArgs<int> eArgs)
        {
            EventHandler<DataChangeEventArgs<int>> eh = StatusChanged;
            if (eh != null)
                eh(this, eArgs);
        }

        int IStatusReportCapable.Status
        {
            get { return this.Status; }
        }

        event EventHandler<DataChangeEventArgs<int>> IStatusReportCapable.StatusChanged
        {
            add { this.StatusChanged += value; }
            remove { this.StatusChanged -= value; }
        }

        bool ICapability.IsAvailable
        {
            get { return this.IsAvailable; }
        }

        event EventHandler<DataChangeEventArgs<bool>> ICapability.IsAvailableChanging
        {
            add { this.IsAvailableChanging += value; }
            remove { this.IsAvailableChanging -= value; }
        }

        event EventHandler<DataChangeEventArgs<bool>> ICapability.IsAvailableChanged
        {
            add { this.IsAvailableChanged += value; }
            remove { this.IsAvailableChanged -= value; }
        }
    }

    internal class ThisCapableEntity : Entity, IThisCapability
    {

        public void DoThis()
        {
            Trace.WriteLine("DoThis!");
        }

        void IThisCapability.DoThis()
        {
            this.DoThis();
        }

        bool ICapability.IsAvailable
        {
            get { return this.IsAvailable; }
        }

        event EventHandler<DataChangeEventArgs<bool>> ICapability.IsAvailableChanging
        {
            add { this.IsAvailableChanging += value; }
            remove { this.IsAvailableChanging -= value; }
        }

        event EventHandler<DataChangeEventArgs<bool>> ICapability.IsAvailableChanged
        {
            add { this.IsAvailableChanged += value; }
            remove { this.IsAvailableChanged -= value; }
        }
    }

    internal class ThatCalableEntity : Entity, IThatCapability
    {

        public void DoThat()
        {
            Trace.WriteLine("Do That!");
        }
    }

    internal class ThisAndThatCapableEntity : Entity, IThisCapability, IThatCapability
    {
        private readonly IThisCapability _thisCapability = new ThisCapableEntity();
        private readonly IThatCapability _thatCapability = new ThatCalableEntity();

        public void DoThis()
        {
            _thisCapability.DoThis();
        }

        public void DoThat()
        {
            _thatCapability.DoThat();
        }

        void IThisCapability.DoThis()
        {
            this.DoThis();
        }

        bool ICapability.IsAvailable
        {
            get { return this._thisCapability.IsAvailable; }
        }

        event EventHandler<DataChangeEventArgs<bool>> ICapability.IsAvailableChanging
        {
            add { this.IsAvailableChanging += value; }
            remove { this._thisCapability.IsAvailableChanging -= value; }
        }

        event EventHandler<DataChangeEventArgs<bool>> ICapability.IsAvailableChanged
        {
            add { this.IsAvailableChanged += value; }
            remove { this.IsAvailableChanged -= value; }
        }

        void IThatCapability.DoThat()
        {
            this._thatCapability.DoThat();
        }
    }


    #endregion test mocks
}

