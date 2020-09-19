using System;
using System.Collections.Generic;

using dotNeat.Common.Patterns;
using dotNeat.Common.Patterns.Structural.Composite;

namespace UnitTest.Common.Patterns.Structural.Composite.Mocks
{
    public class ConfigValue<TID, TValue> 
        : Config
        , IEntity<TID>
        , ILeaf<ConfigValue<TID, TValue>, Config>
        where TID : IComparable
    {
        private readonly TID _configID;

        public ConfigValue(TID id) 
            : this(id, default(TValue))
        {
        }

        public ConfigValue(TID id, TValue value) 
        {
            this._configID = id;
            this.Value = value;
        }

        public TID ID => this._configID;

        object IEntity.ID => this.ID;

        public TValue Value {get;set;}

        protected override bool IsThisValid()
        {
            return true;
        }

        public override IEnumerable<Config> GetComponents()
        {
            return Array.Empty<Config>();
        }
    }
}
