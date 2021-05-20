using System;
using System.Collections.Generic;
using marketplace.domain.kernal;
namespace marketplace.domain.entities
{
    public class UserId:ValueObject
    {
        // public Guid Value => _value;
        private readonly Guid _value;

        public UserId(Guid value)
        {
            if(value==default)
            {
                throw new ArgumentException("Owner id must be specified", nameof(value));
            }
            _value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }

        public static implicit operator Guid(UserId self) => self._value;
    }
}