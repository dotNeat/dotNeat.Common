using System;
using System.Collections.Generic;

namespace dotNeat.Common.Patterns.Structural.Composite
{
    public interface IComposite 
        : IComponent
    {
        IComposite Add(IComponent component);
        
        IComposite Remove(IComponent component);

        IComposite RemoveAllComponents();
    }

    public interface IComposite<TComponent>
        : IComponent<TComponent>
        , IComposite
        where TComponent : IComponent<TComponent>
    {
        IComposite<TComponent> Add(TComponent component);
        
        IComposite<TComponent> Remove(TComponent component);

        new IComposite<TComponent> RemoveAllComponents();
    }

    public interface IEntityComposite<TComposite, TComponent, TComponentID>
        : IComposite<TComponent>
        , IEnumerable<TComponent>
        , IDictionary<TComponentID,TComponent>
        where TComposite : IEntityComposite<TComposite, TComponent, TComponentID>, TComponent
        where TComponent : IComponent<TComponent>
        where TComponentID : IComparable //IEquatable<TID>
    {
        IReadOnlyCollection<TComponentID> GetComponentIDs();
    }
}
