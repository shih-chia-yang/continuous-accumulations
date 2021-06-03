using System;
using System.Collections.Generic;
using marketplace.domain.kernel;

namespace marketplace.domain.AggregateModels.UserAggregate
{
    public class FullName : ValueObject
    {
        public string Value { get; internal set; }

        protected FullName(){}
        internal FullName(string fullName)
        {
            Value = fullName;
        }

        public static FullName FromString(string fullName)
        {
            if(string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException(nameof(fullName));
            }
            return new FullName(fullName);
        }

        public static implicit operator string(FullName fullName) => fullName.Value;
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}