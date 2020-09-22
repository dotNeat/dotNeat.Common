using System;
using System.Collections.Generic;
using System.Text;

using dotNeat.Common.Patterns.Structural.Composite;

namespace UnitTest.Common.Patterns.Structural.Composite.Mocks
{
    public class ConfigValue<TValue> 
        : Config
        , ILeaf<ConfigValue<TValue>, Config>
    {

        public ConfigValue(Enum id) 
            : this(id, default(TValue))
        {
        }

        public ConfigValue(Enum id, TValue value) 
            : base(id)
        {
            this.Value = value;
        }


        public TValue Value {get;set;}

        protected override bool IsThisValid()
        {
            return true;
        }

        public override IEnumerable<Config> GetComponents()
        {
            return Array.Empty<Config>();
        }

        public override void AppendToStringBuilder(StringBuilder stringBuilder,string indentation)
        {
            stringBuilder.AppendLine($"{indentation}{this.ID} : {this.Value.ToString()}");
        }
    }
}
