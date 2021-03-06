using System.Collections.Generic;
using marketplace.domain.kernel;

namespace marketplace.domain.AggregateModels.ClassifiedAdAggregate
{
    public class ClassifiedAdText : ValueObject
    {
        public string Value{ get; internal set;}

        protected ClassifiedAdText() { }
        internal ClassifiedAdText(string value)
        {
            Value = value;
        }
        public static ClassifiedAdText FromString(string value) => new ClassifiedAdText(value);

        public static ClassifiedAdText NoText() => new ClassifiedAdText();
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator string(ClassifiedAdText text) => text.Value;
    }
}