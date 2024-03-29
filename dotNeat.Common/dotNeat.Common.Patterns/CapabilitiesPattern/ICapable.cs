﻿namespace dotNeat.Common.Patterns.CapabilitiesPattern
{
    using System;

    /// <summary>
    /// Defines an interface of a composite object that is expected to host
    /// a collection of specific ICapabilities
    /// either by implementing some or all of the capabilities by itself
    /// or by delegating the capabilities hosted by its composite child objects.
    /// </summary>
    public interface ICapable 
    {
        bool Has<TCapability>() 
            where TCapability : ICapability;

        TCapability[] GetImplementationsOf<TCapability>()
            where TCapability : ICapability;

        ICapability[] GetAllCapabilityImplementations();

        Type[] GetAllImplementedCapabilityTypes();
    }
}
