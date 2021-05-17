using System;
using System.Collections.Generic;
using marketplace.domain.kernal;

namespace marketplace.domain
{
    public class UserId:ValueObject
    {
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
    }
}