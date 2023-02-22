namespace dotNeat.Common.Patterns.CapabilitiesPattern
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;

    using dotNeat.Common.Patterns.EventsPattern;
    using dotNeat.Common.Patterns.GoF.Structural.Composite;

    /// <summary>
    /// Implements composable data structure of objects implementing various capabilities
    /// allowing all the current capabilities of what is considered a root object to discoverable
    /// and accessible on per-specific-capability basis.
    /// </summary>
    public abstract class CapabilitiesHost
        : IComponent<CapabilitiesHost>
        , ICapable
        , ICapability
    {
        //private readonly ObservableCollection<ICapability> _capabilities = null;
        //private readonly ReadOnlyObservableCollection<ICapability> _readOnlyCapabilities = null;

        protected internal CompositeCapabilitiesHost? _container;
        protected internal readonly Dictionary<Type, List<ICapability>>? _capabilityImplementationsByCapability = null;

        public CapabilitiesHost(CompositeCapabilitiesHost? container = null)
        {
            //_capabilities = new ObservableCollection<ICapability>();
            //_readOnlyCapabilities = new ReadOnlyObservableCollection<Capability>(_capabilities);

            this._container = container;

            this._capabilityImplementationsByCapability = new Dictionary<Type, List<ICapability>>();

            //register this instance's own capabilities:
            Type thisType = this.GetType();
            //IEnumerable<Type> thisTypeCapabilities = thisType.GetInterfaces().Where(i => typeof(ICapability).IsAssignableFrom(i));
            IEnumerable<Type> thisTypeCapabilities = thisType.GetInterfaces().Where(i => i.GetInterfaces().Contains(typeof(ICapability)));
            foreach (Type t in thisTypeCapabilities)
            {
                this._capabilityImplementationsByCapability.Add(t, new List<ICapability>(new ICapability[] {this,}));
            }

            if (this._container != null)
            {
                this._container.Add(this);
            }
        }

        #region IComponent<CapabilitiesHost>

        public IComposite<CapabilitiesHost>? Container => this._container;

        public IEnumerable<CapabilitiesHost> GetComponents()
        {
            return Array.Empty<CapabilitiesHost>();
        }

        #endregion IComponent<CapabilitiesHost>


        #region IComponent<CapabilitiesHost> explicit

        IComposite IComponent.Container => this.Container;

        IEnumerable<IComponent> IComponent.GetComponents()
        {
            return this.GetComponents();
        }

        #endregion IComponent<CapabilitiesHost> explicit

        #region ICapable

        public bool Has<TCapability>() 
            where TCapability : ICapability
        {
            Type capabilityType = typeof(TCapability);
            return 
                _capabilityImplementationsByCapability.ContainsKey(capabilityType) 
                && (_capabilityImplementationsByCapability[capabilityType].Count > 0)
                ;
        }

        public TCapability[] GetImplementationsOf<TCapability>() 
            where TCapability : class, ICapability
        {
            Type capabilityType = typeof(TCapability);
            List<ICapability> capabilityImplementations = null;
            if (_capabilityImplementationsByCapability.TryGetValue(capabilityType, out capabilityImplementations))
                return capabilityImplementations.Cast<TCapability>().ToArray();
            else
                return new TCapability[0];
        }

        public ICapability[] GetAllCapabilityImplementations()
        {
            List<ICapability> capabilityImplementations = new List<ICapability>(_capabilityImplementationsByCapability.Count);
            foreach (var key in _capabilityImplementationsByCapability.Keys)
                capabilityImplementations.AddRange(_capabilityImplementationsByCapability[key]);
            return capabilityImplementations.ToArray();
        }

        public Type[] GetAllImplementedCapabilityTypes()
        {
            return _capabilityImplementationsByCapability.Keys.ToArray();
        }

        //public ReadOnlyObservableCollection<ICapability> Capabilities
        //{
        //    get { return _readOnlyCapabilities; }
        //}

        //public TCapability Get<TCapability>() where TCapability : class, ICapability
        //{
        //    return _readOnlyCapabilities.FirstOrDefault(i => i is TCapability) as TCapability;
        //}

        //public ICapable GetFirstActualImplementationOf<TCapability>() where TCapability : ICapability
        //{
        //    ICapable[] result = GetAllActualImplementationsOf<TCapability>();
        //    if (result.Length > 0)
        //        return result[0];
        //    else
        //        return null;
        //}

        //public ICapable[] GetAllActualImplementationsOf<TCapability>() where TCapability : ICapability
        //{
        //    List<ICapable> result = null;
        //    if (!_actualCapabilityHostsByCapability.TryGetValue(typeof(TCapability), out result))
        //        result = new List<ICapable>();
        //    return result.ToArray();
        //}

        #endregion ICapable

        #region ICapable explicit

        bool ICapable.Has<TCapability>()
        {
            return this.Has<TCapability>();
        }

        TCapability[] ICapable.GetImplementationsOf<TCapability>()
        {
            return this.GetImplementationsOf<TCapability>();
        }

        ICapability[] ICapable.GetAllCapabilityImplementations()
        {
            return this.GetAllCapabilityImplementations();
        }

        Type[] ICapable.GetAllImplementedCapabilityTypes()
        {
            return this.GetAllImplementedCapabilityTypes();
        }

        #endregion ICapable explicit

        #region ICapability

        public virtual bool IsAvailable
        {
            get { return true; }
        }

        public event EventHandler<DataChangeEventArgs<bool>> IsAvailableChanging;

        public event EventHandler<DataChangeEventArgs<bool>> IsAvailableChanged;

        #endregion ICapability

        #region ICapability explicit

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

        #endregion ICapability explicit

    }


    public abstract class CompositeCapabilitiesHost
        : CapabilitiesHost
        , IComposite<CapabilitiesHost>
        , ICapable
        , ICapability
    {
        private const int initialComponentsCapacity = 3;

        private readonly HashSet<CapabilitiesHost> _components = new(initialComponentsCapacity);

        public CompositeCapabilitiesHost Add(CapabilitiesHost component)
        {
            if (component.Container == this)
            {
                Debug.Assert(this._components.Contains(component), "Oops, the component does not exist in this container.");
                return this;
            }

            Debug.Assert(component.Container == null, "The component already belongs to another conatiner.");

            foreach (var capability in component._capabilityImplementationsByCapability.Keys)
            {
                List<ICapability> capabilityImplementations = null;
                if (!this._capabilityImplementationsByCapability.TryGetValue(capability, out capabilityImplementations))
                {
                    capabilityImplementations = new List<ICapability>();
                    this._capabilityImplementationsByCapability.Add(capability, capabilityImplementations);
                }
                foreach (var capabilityImplementation in component._capabilityImplementationsByCapability[capability])
                {
                    if (!capabilityImplementations.Contains(capabilityImplementation))
                    {
                        capabilityImplementations.Add(capabilityImplementation);
                    }
                }
            }

            component._container = this;
            this._components.Add(component);
            return this;
        }

        public CompositeCapabilitiesHost Remove(CapabilitiesHost component)
        {
            if (component.Container != this)
            {
                Debug.Assert(!this._components.Contains(component), "Oops, the component actually exists in this container.");
                return this; // nothing needs removal...
            }

            Debug.Assert(component.Container == this, "The component about to remove does not belongs to this conatiner.");
            Debug.Assert(this._components.Contains(component), "The componentabout to remove does not exists in this container.");

            foreach (var capability in component._capabilityImplementationsByCapability.Keys)
            {
                List<ICapability> capabilityImplementations = null;
                if (this._capabilityImplementationsByCapability.TryGetValue(capability, out capabilityImplementations))
                {
                    //List<ICapability> capabilityImplementationsToRemove = child._capabilityImplementationsByCapability[capability];
                    //int i = capabilityImplementations.Count;
                    //while(--i >= 0)
                    //{
                    //    if (capabilityImplementationsToRemove.Contains(capabilityImplementations.ElementAt(i)))
                    //        capabilityImplementations.Remove(capabilityImplementations.ElementAt(i));
                    //}

                    int initialCapabilityImplementationsCount =
                        capabilityImplementations.Count;
                    foreach (ICapability capabilityImplementation in component._capabilityImplementationsByCapability[capability])
                    {
                        capabilityImplementations.Remove(capabilityImplementation);
                    }
                    int finalCapabilityImplementationsCount =
                        capabilityImplementations.Count;
                    Debug.Assert(
                        (initialCapabilityImplementationsCount - finalCapabilityImplementationsCount)
                        == component._capabilityImplementationsByCapability[capability].Count,
                        "Component capabilities removal went wrong!"
                        );
                    if (finalCapabilityImplementationsCount == 0)
                    {
                        this._capabilityImplementationsByCapability.Remove(capability);
                    }
                }
                else
                {
                    Debug.Assert(false, "Something went really wrong!");
                }
            }

            this._components.Remove(component);
            return this;
        }

        public IComposite Add(IComponent component)
        {
            Debug.Assert(component is CapabilitiesHost, "Unexpected IComponent type to add!");

            return this.Add((CapabilitiesHost)component);
        }

        public IComposite Remove(IComponent component)
        {
            Debug.Assert(component is CapabilitiesHost, "Unexpected IComponent type to remove!");

            return this.Remove((CapabilitiesHost)component);
        }

        public IComposite<CapabilitiesHost> RemoveAllComponents()
        {
            var components = this._components.ToArray();
            foreach(var component in components)
            {
                this._components.Remove(component);
            }

            return this;
        }

        IComposite<CapabilitiesHost> IComposite<CapabilitiesHost>.Add(CapabilitiesHost component)
        {
            return this.Add(component);
        }

        IComposite<CapabilitiesHost> IComposite<CapabilitiesHost>.Remove(CapabilitiesHost component)
        {
            return this.Remove(component);
        }

        IComposite IComposite.RemoveAllComponents()
        {
            return this.RemoveAllComponents();
        }
    }



    /// <summary>
    /// THIS IMPLEMENTATION IS NOT COMPLETE!!! 
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    //public abstract class CapabilitiesHost<TData> : TreeNode<TData>, ITreeNode<TData>, ICapable
    //    where TData : IComparable<TData>
    //{
    //    private readonly ObservableCollection<ICapability> _capabilities = null;
    //    private readonly ReadOnlyObservableCollection<ICapability> _readOnlyCapabilities = null;
    //    private readonly Dictionary<Type, List<ICapable>> _actualCapabilityHostsByCapability = null;
        
    //    public CapabilitiesHost(TData data)
    //        : base(data)
    //    {
    //        _capabilities = new ObservableCollection<ICapability>();
    //        _readOnlyCapabilities = new ReadOnlyObservableCollection<ICapability>(_capabilities);
    //        _actualCapabilityHostsByCapability = new Dictionary<Type, List<ICapable>>();
    //    }

    //    #region ICapable

    //    public ReadOnlyObservableCollection<ICapability> Capabilities
    //    {
    //        get { return _readOnlyCapabilities; }
    //    }

    //    public bool Has<TCapability>() where TCapability : ICapability
    //    {
    //        return _readOnlyCapabilities.Any(i => i is TCapability);
    //    }

    //    public TCapability Get<TCapability>() where TCapability : class, ICapability
    //    {
    //        return _readOnlyCapabilities.FirstOrDefault(i => i is TCapability) as TCapability;
    //    }

    //    public ICapable GetFirstActualImplementationOf<TCapability>() where TCapability : ICapability
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public ICapable[] GetAllActualImplementationsOf<TCapability>() where TCapability : ICapability
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion ICapable


    //    bool ICapable.Has<TCapability>()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    TCapability[] ICapable.GetImplementationsOf<TCapability>()
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public ICapability[] GetAllCapabilityImplementations()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Type[] GetAllImplementedCapabilityTypes()
    //    {
    //        throw new NotImplementedException();
    //    }


    //    ICapability[] ICapable.GetAllCapabilityImplementations()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    Type[] ICapable.GetAllImplementedCapabilityTypes()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
