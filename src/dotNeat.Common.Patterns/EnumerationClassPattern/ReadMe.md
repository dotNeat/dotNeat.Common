#Enumeration Class Pattern

## Overview

Enumerations (or enum types for short) are a thin language wrapper around an
integral type. You might want to limit their use to when you are storing one
value from a closed set of values. Classification based on sizes (small, medium,
large) is a good example. Using enums for control flow or more robust
abstractions can be a code smell. This type of usage leads to fragile code with
many control flow statements checking values of the enum.

Instead, you can create Enumeration classes that enable all the rich features
of an object-oriented language.

However, this isn't a critical topic and in many cases, for simplicity, you can
still use regular enum types if that's your preference. The use of enumeration
classes is more related to business-related concepts.

## References
https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
