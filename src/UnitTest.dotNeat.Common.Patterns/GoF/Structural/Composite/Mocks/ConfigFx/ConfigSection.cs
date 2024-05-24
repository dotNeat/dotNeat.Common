namespace UnitTest.dotNeat.Common.Patterns.GoF.Structural.Composite.Mocks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    using dotNeat.Common.Patterns.GoF.Structural.Composite;

    using global::dotNeat.Common.Patterns.GoF.Structural.Composite;

    public class ConfigSection 
        : Config
        , IEntityComposite<ConfigSection, Config, Enum>
    {
        private readonly IDictionary<Enum, Config> _childComponents = new Dictionary<Enum, Config>();

        public ConfigSection() 
            : base()
        {
        }

        public ConfigSection(Enum id) 
            : base(id)
        {
        }

        public override IEnumerable<Config> GetComponents()
        {
            return this._childComponents.Values;
        }

        protected override bool IsThisValid()
        {
            return true;
        }

        public IReadOnlyCollection<Enum> GetComponentIDs()
        {
            return (IReadOnlyCollection<Enum>)this._childComponents.Keys;
        }

        public IComposite<Config> Add(Config component)
        {
            this._childComponents[component.ID] = component;
            return this;
        }

        public IComposite<Config> Remove(Config component)
        {
            this._childComponents.Remove(component.ID);
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

        public void Add(Enum key, Config value)
        {
            this.EnsureKeyMatch(key, value);
            this._childComponents.Add(key, value);
        }

        public bool ContainsKey(Enum key)
        {
            return this._childComponents.ContainsKey(key);
        }

        public bool Remove(Enum key)
        {
            return this._childComponents.Remove(key);
        }

        public bool TryGetValue(Enum key,[MaybeNullWhen(false)] out Config value)
        {
            bool result =  this._childComponents.TryGetValue(key, out value);
            if(result)
            {
                this.EnsureKeyMatch(key, value);
            }
            return result;
        }

        public Config this[Enum key] { get => this._childComponents[key]; set => this._childComponents[key] = value; }

        public ICollection<Enum> Keys => this._childComponents.Keys;

        public ICollection<Config> Values => this._childComponents.Values;

        public void Add(KeyValuePair<Enum, Config> item)
        {
            this.EnsureKeyMatch(item.Key, item.Value);
            this._childComponents.Add(item);
        }

        public void Clear()
        {
            this._childComponents.Clear();
        }

        public bool Contains(KeyValuePair<Enum,Config> item)
        {
            return this._childComponents.Contains(item);
        }

        public void CopyTo(KeyValuePair<Enum,Config>[] array, int arrayIndex)
        {
            this._childComponents.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<Enum,Config> item)
        {
            this.EnsureKeyMatch(item.Key, item.Value);
            return this._childComponents.Remove(item);
        }

        public int Count => this._childComponents.Count;

        public bool IsReadOnly => false;

        IEnumerator<KeyValuePair<Enum,Config>> IEnumerable<KeyValuePair<Enum,Config>>.GetEnumerator()
        {
            return this._childComponents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._childComponents.GetEnumerator();
        }

        private void EnsureKeyMatch(Enum key, Config? component)
        {
            if (component != null)
            {
                Debug.Assert(key.GetType().FullName == component.ID.GetType().FullName, $"The {nameof(component.ID)} type must match the {nameof(key)} type.");
                Debug.Assert(key == component.ID, $"The {nameof(component.ID)} must match the {nameof(key)}.");
            }
        }

        public override void AppendToStringBuilder(StringBuilder stringBuilder,string indentation)
        {
            stringBuilder.AppendLine($"{indentation}{this.ID} :");
            foreach(Config component in this.GetComponents())
            {
                component.AppendToStringBuilder(stringBuilder, indentation + Config.SingleIndent);
            }
        }
    }
}
