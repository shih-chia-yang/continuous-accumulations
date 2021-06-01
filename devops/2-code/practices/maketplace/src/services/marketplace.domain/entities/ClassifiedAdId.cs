using System;
using System.Collections.Generic;
using marketplace.domain.kernel;

namespace marketplace.domain.entities
{
    public class ClassifiedAdId : ValueObject
    {
        public Guid Value{ get; internal set; }

        protected ClassifiedAdId(){}
        public ClassifiedAdId(Guid value)
        {
            if(value==default)
            {
                throw new ArgumentNullException(nameof(value), "Classified Ad id cannot be empty");
            }
            Value = value;
        }

        public static implicit operator Guid(ClassifiedAdId self) => self.Value;

        public static implicit operator ClassifiedAdId(string value) =>
            new ClassifiedAdId(Guid.Parse(value));

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}