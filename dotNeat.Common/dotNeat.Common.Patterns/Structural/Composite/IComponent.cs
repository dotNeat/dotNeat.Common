using System.Collections.Generic;

namespace dotNeat.Common.Patterns.Structural.Composite
{
    public interface IComponent
    {
        IComposite Parent {get;}

        IEnumerable<IComponent> GetComponents();
    }

    public interface IComponent<TComponent>
        : IComponent
        where TComponent : IComponent<TComponent>, IComponent
    {
        new IComposite<TComponent> Parent {get;}

        new IEnumerable<TComponent> GetComponents();
    }
}
