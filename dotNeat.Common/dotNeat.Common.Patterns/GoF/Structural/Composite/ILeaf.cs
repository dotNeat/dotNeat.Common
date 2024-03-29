﻿namespace dotNeat.Common.Patterns.GoF.Structural.Composite
{
    public interface ILeaf
        : IComponent
    {
    }

    public interface ILeaf<TLeaf, TComponent>
        : IComponent<TComponent>
        , ILeaf
        where TLeaf : ILeaf<TLeaf, TComponent>, TComponent
        where TComponent : IComponent<TComponent>
    {
    }
}
