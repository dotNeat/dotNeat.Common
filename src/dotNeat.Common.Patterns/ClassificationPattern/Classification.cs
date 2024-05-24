namespace dotNeat.Common.Patterns.ClassificationPattern
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Collections.Concurrent;
    using dotNeat.Common.Utilities;

    public class Classification
        : IClassification
    {
        private static readonly ConcurrentDictionary<string, Classification> classificationsByID =
            new ConcurrentDictionary<string, Classification>();

        public static Classification MatchClassification(params object[] classifierObjects)
        {
            return Classification.MatchClassification(classifierObjects.AsEnumerable());
        }

        public static Classification MatchClassification(IEnumerable<object> classifierObjects)
        {
            List<Classifier> classifiers = new List<Classifier>(classifierObjects.Count());
            foreach (var item in classifierObjects)
            {
                classifiers.Add(Classifier.MatchClassifier(item));
            }
            return Classification.MatchClassification(classifiers);
        }

        public static Classification MatchClassification(params Classifier[] classifiers)
        {
            return Classification.MatchClassification(classifiers.AsEnumerable());
        }

        public static Classification MatchClassification(IEnumerable<Classifier> classifiers)
        {
            Classification classification = new Classification(classifiers);
            classification = Classification.classificationsByID.GetOrAdd(classification.ID, classification);
            return classification;
        }

        private readonly ICollection<Classifier> _classifiers;
        private readonly ICollection<Type> _classifierTypes;

        private Classification()
        {
            this._classifiers = new HashSet<Classifier>();
            this._classifierTypes = new HashSet<Type>();
            this.ID = this.GenerateID(this._classifiers);
        }

        protected Classification(params Classifier[] classifiers)
            : this(classifiers.AsEnumerable())
        {
        }

        protected Classification(IEnumerable<Classifier> classifiers)
        {
            if (classifiers == null)
            {
                this._classifiers = new HashSet<Classifier>();
                this._classifierTypes = new HashSet<Type>();
            }
            else
            {
                Assumption.AssertTrue(classifiers.Count() == classifiers.Distinct().Count(), nameof(classifiers));

                this._classifiers = new HashSet<Classifier>(classifiers);

                var classifierTypes = this._classifiers.Select(i => i.ClassifierObject.GetType()).ToList();
                this._classifierTypes = new HashSet<Type>(classifierTypes);

                Assumption.AssertEqual(this._classifierTypes.Count, this._classifiers.Count, nameof(this._classifierTypes.Count));
            }

            this.ID = this.GenerateID(this._classifiers);
        }

        protected string GenerateID(IEnumerable<Classifier> classifiers)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var classifier in classifiers)
            {
                if (sb.Length == 0)
                {
                    sb.Append(classifier.ID);
                }
                else
                {
                    sb.Append(" + " + classifier.ID);
                }
            }

            return sb.ToString();
        }

        public int ClassifiersCount { get { return this._classifiers.Count; } }

        public IEnumerable<IClassifier> Classifiers { get { return this._classifiers; } }

        public IEnumerable<Type> ClassifierTypes { get { return this._classifierTypes; } }

        public string ID { get; }

        object IIdentifiable.ID { get { return this.ID; } }

        public IEnumerable<string> GetAllKnownIDs()
        {
            return Classification.classificationsByID.Keys;
        }

        IEnumerable<object> IIdentifiable.GetAllKnownIDs()
        {
            return this.GetAllKnownIDs();
        }
    }
}