namespace dotNeat.Common.Patterns.ClassificationPattern
{
    using System;

    public interface IClassifier
        : IIdentifiable<string>
    {
        Type ClassifierType { get; }
        object ClassifierObject { get; }
    }
}