using System;
using System.Collections.Generic;
using System.Text;

using dotNeat.Common.Patterns;
using dotNeat.Common.Patterns.Structural.Composite;

namespace UnitTest.Common.Patterns.Structural.Composite.Mocks
{
    public abstract class Config
        : EntityBase<Enum>
        , IComponent<Config>
        , IComponent
    {
        public enum Identity
        {
            Root,
        }

        protected Config() 
            : base(Identity.Root)
        {
        }

        protected Config(Enum id) 
            : base(id)
        {
        }

        public IComposite<Config> Parent
        {
            get;
            protected set;
        }

        public abstract IEnumerable<Config> GetComponents();

        IComposite IComponent.Parent => this.Parent;

        IEnumerable<IComponent> IComponent.GetComponents()
        {
            return this.GetComponents();
        }

        public bool IsValid()
        {
            bool result = this.IsThisValid();
            if(!result)
            {
                return result;
            }

            foreach(var component in this.GetComponents())
            {
                result &= component.IsValid();
                if(!result)
                {
                    return result;
                }
            }

            return result;
        }

        protected abstract bool IsThisValid();

        protected const string SingleIndent = "  ";

        public string RenderAsString(int indentCount = 0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            while(indentCount > 0)
            {
                stringBuilder.Append(Config.SingleIndent);
                indentCount--;
            }
            this.AppendToStringBuilder(stringBuilder, stringBuilder.ToString());
            return stringBuilder.ToString();
        }

        public abstract void AppendToStringBuilder(StringBuilder stringBuilder, string indentation);
    }
}
