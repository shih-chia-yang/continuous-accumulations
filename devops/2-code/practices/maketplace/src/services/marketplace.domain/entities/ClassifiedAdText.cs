using System.Collections.Generic;
using marketplace.domain.kernal;

namespace marketplace.domain.entities
{
    public class ClassifiedAdText : ValueObject
    {
        public string Value{ get; }

        internal ClassifiedAdText(string value)
        {
            Value = value;
        }
        public static ClassifiedAdText FromString(string value) => new ClassifiedAdText(value);
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator string(ClassifiedAdText text) => text.Value;
    }
}