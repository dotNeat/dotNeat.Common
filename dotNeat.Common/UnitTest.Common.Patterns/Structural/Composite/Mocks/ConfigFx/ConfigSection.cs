using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using dotNeat.Common.Patterns;
using dotNeat.Common.Patterns.Structural.Composite;

using NuGet.Frameworks;

namespace UnitTest.Common.Patterns.Structural.Composite.Mocks
{
    public class ConfigSection<TID, TComponentID> 
        : Config
        , IEntity<TID>
        , IEntityComposite<ConfigSection<TID, TComponentID>, Config, TComponentID>
        where TID : IComparable //IEquatable<TID>
        where TComponentID : IComparable
    {
        private readonly TID _configID;
        private readonly IDictionary<TComponentID, Config> _childComponents;

        public ConfigSection(TID configID) 
        {
            this._configID = configID;

            Type childComponentsIDType = typeof(TComponentID);
            if(childComponentsIDType.IsEnum)
            {
                int capacity = Enum.GetValues(childComponentsIDType).Length;
                _childComponents = new Dictionary<TComponentID,Config>(capacity);
            }
            else
            {
                _childComponents = new Dictionary<TComponentID,Config>();
            }
        }

        public TID ID => this._configID;

        object IEntity.ID => this.ID;

        public override IEnumerable<Config> GetComponents()
        {
            return this._childComponents.Values;
        }

        protected override bool IsThisValid()
        {
            return true;
        }

        public IReadOnlyCollection<TComponentID> GetComponentIDs()
        {
            return (IReadOnlyCollection<TComponentID>)this._childComponents.Keys;
        }

        public IComposite<Config> Add(Config component)
        {
            var entity = this.EnsureEntityLikeComponent(component);
            this._childComponents[entity.ID] = component;
            return this;
        }

        public IComposite<Config> Remove(Config component)
        {
            var entity = this.EnsureEntityLikeComponent(component);
            this._childComponents.Remove(entity.ID);
            return this;
        }

        public IComposite<Config> RemoveAllComponents()
        {
            this._childComponents.Clear();
            return this;
        }

        public IComposite Add(IComponent component)
        {
            return this.Add(component);
        }

        public IComposite Remove(IComponent component)
        {
            return this.Remove(component);
        }

        IComposite IComposite.RemoveAllComponents()
        {
            return this.RemoveAllComponents();
        }

        public IEnumerator<Config> GetEnumerator()
        {
            return this._childComponents.Values.GetEnumerator();
        }

        public void Add(TComponentID key, Config value)
        {
            Debug.Assert((value as IEntity<TComponentID>)  != null, $"The {nameof(value)} must be an {typeof(IEntity<TComponentID>).FullName}");
            Debug.Assert((value as IEntity<TComponentID>).ID.CompareTo(key) == 0, $"The {nameof(value)} ID must match as the {nameof(key)}");

            this._childComponents.Add(key, value);
        }

        public bool ContainsKey(TComponentID key)
        {
            return this._childComponents.ContainsKey(key);
        }

        public bool Remove(TComponentID key)
        {
            return this._childComponents.Remove(key);
        }

        public bool TryGetValue(TComponentID key,[MaybeNullWhen(false)] out Config value)
        {
            bool result =  this._childComponents.TryGetValue(key, out value);
            if(result)
            {
                this.EnsureKeyMatch(key, value);
            }
            return result;
        }

        public Config this[TComponentID key] { get => this._childComponents[key]; set => this._childComponents[key] = value; }

        public ICollection<TComponentID> Keys => this._childComponents.Keys;

        public ICollection<Config> Values => this._childComponents.Values;

        public void Add(KeyValuePair<TComponentID,Config> item)
        {
            this.EnsureKeyMatch(item.Key, item.Value);
            this._childComponents.Add(item);
        }

        public void Clear()
        {
            this._childComponents.Clear();
        }

        public bool Contains(KeyValuePair<TComponentID,Config> item)
        {
            return this._childComponents.Contains(item);
        }

        public void CopyTo(KeyValuePair<TComponentID,Config>[] array, int arrayIndex)
        {
            this._childComponents.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TComponentID,Config> item)
        {
            this.EnsureKeyMatch(item.Key, item.Value);
            return this._childComponents.Remove(item);
        }

        public int Count => this._childComponents.Count;

        public bool IsReadOnly => false;

        IEnumerator<KeyValuePair<TComponentID,Config>> IEnumerable<KeyValuePair<TComponentID,Config>>.GetEnumerator()
        {
            return this._childComponents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._childComponents.GetEnumerator();
        }

        private IEntity<TComponentID> EnsureEntityLikeComponent(Config component)
        {
            var entity = component as IEntity<TComponentID>;
            Debug.Assert(entity != null, $"The {nameof(component)} must be an {typeof(IEntity<TComponentID>).FullName}");

            if(entity == null)
            {
                throw new ArgumentException($"Provided {nameof(component)} must be an IEntity!");
            }

            return entity;
        }

        private void EnsureKeyMatch(TComponentID key, Config component)
        {
            if (component != null)
            {
                Debug.Assert((component as IEntity<TComponentID>)  != null, $"The {nameof(component)} must be an {typeof(IEntity<TComponentID>).FullName}");
                Debug.Assert((component as IEntity<TComponentID>).ID.CompareTo(key) == 0, $"The {nameof(component)} ID must match as the {nameof(key)}");
            }
        }

    }
}
