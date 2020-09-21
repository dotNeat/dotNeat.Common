using System;
using System.Collections.Generic;

using dotNeat.Common.Patterns;
using dotNeat.Common.Patterns.Structural.Composite;

namespace UnitTest.Common.Patterns.Structural.Composite.Mocks
{
    public abstract class Config
        : IComponent<Config>
        , IComponent
    {
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

    }
}
