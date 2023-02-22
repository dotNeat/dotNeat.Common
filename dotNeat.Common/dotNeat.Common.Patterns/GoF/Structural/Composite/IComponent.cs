namespace dotNeat.Common.Patterns.GoF.Structural.Composite
{
    using System.Collections.Generic;

    public interface IComponent
    {
        IComposite? Container { get;}

        IEnumerable<IComponent> GetComponents();
    }

    public interface IComponent<TComponent>
        : IComponent
        where TComponent : IComponent<TComponent>, IComponent
    {
        new IComposite<TComponent>? Container {get;}

        new IEnumerable<TComponent> GetComponents();
    }
}
