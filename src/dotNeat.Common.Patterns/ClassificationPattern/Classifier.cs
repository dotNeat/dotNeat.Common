namespace dotNeat.Common.Patterns.ClassificationPattern
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class Classifier
        : IClassifier
    {
        private static readonly ConcurrentDictionary<string, Classifier> classifiersByID =
            new ConcurrentDictionary<string, Classifier>();

        public static Classifier MatchClassifier(object classifierObject)
        {
            Classifier classifier = new Classifier(classifierObject);
            classifier = Classifier.classifiersByID.GetOrAdd(classifier.ID, classifier);
            return classifier;
        }

        private Classifier()
            : this(new object())
        {
        }

        protected Classifier(object classifierObject)
        {
            this.ClassifierObject = classifierObject;
            this.ClassifierType = classifierObject.GetType();
            this.ID = GenerateClassifierID(classifierObject);
        }

        public object ClassifierObject { get; }

        public Type ClassifierType { get; }

        public string ID { get; }

        object IIdentifiable.ID { get { return this.ID; } }

        public IEnumerable<string> GetAllKnownIDs() { return Classifier.classifiersByID.Keys; }

        IEnumerable<object> IIdentifiable.GetAllKnownIDs() { return this.GetAllKnownIDs(); }

        protected string GenerateClassifierID(object classifierObject)
        {
            return classifierObject.GetType().FullName + ": " + classifierObject;
        }
    }
}