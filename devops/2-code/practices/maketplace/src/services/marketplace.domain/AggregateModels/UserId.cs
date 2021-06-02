using System;
using System.Collections.Generic;
using marketplace.domain.kernel;
namespace marketplace.domain.AggregateModels
{
    public class UserId : ValueObject
    {
        public static UserId NoUser() => new UserId();
        public Guid Value { get; internal set;}
        protected UserId(){}

        public UserId(Guid value)
        {
            if(value==default)
            {
                throw new ArgumentException("Owner id must be specified", nameof(value));
            }
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static implicit operator Guid(UserId self) => self.Value;
    }
}