#Value Object Pattern

## Overview

Identity is fundamental for entities. However, there are many objects and data
items in a system that do not require an identity and identity tracking, such as
value objects.

A value object can reference other entities. For example, in an application that
generates a route that describes how to get from one point to another, that
route would be a value object. It would be a snapshot of points on a specific
route, but this suggested route would not have an identity, even though
internally it might refer to entities like City, Road, etc.

## Important characteristics of value objects

There are two main characteristics for value objects:
    - They have no identity.
    - They are immutable.

The first characteristic was already discussed.
Immutability is an important requirement. The values of a value object must be
immutable once the object is created. Therefore, when the object is constructed,
you must provide the required values, but you must not allow them to change
during the object's lifetime.

Value objects allow you to perform certain tricks for performance, thanks to
their immutable nature. This is especially true in systems where there may be
thousands of value object instances, many of which have the same values. Their
immutable nature allows them to be reused; they can be interchangeable objects,
since their values are the same and they have no identity. This type of
optimization can sometimes make a difference between software that runs slowly
and software with good performance. Of course, all these cases depend on the
application environment and deployment context.

## References
https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
