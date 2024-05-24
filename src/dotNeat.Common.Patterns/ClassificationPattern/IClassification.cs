namespace dotNeat.Common.Patterns.ClassificationPattern
{
    using System;
    using System.Collections.Generic;

    public interface IClassification
        : IIdentifiable<string>
    {
        int ClassifiersCount { get; }
        IEnumerable<IClassifier> Classifiers { get; }
        IEnumerable<Type> ClassifierTypes { get; }
    }
}