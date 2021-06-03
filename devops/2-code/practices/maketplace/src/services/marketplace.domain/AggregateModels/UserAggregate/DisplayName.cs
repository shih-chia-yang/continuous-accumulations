using System;
using System.Collections.Generic;
using marketplace.domain.exceptions;
using marketplace.domain.kernel;

namespace marketplace.domain.AggregateModels.UserAggregate
{
    public delegate bool CheckTextForProfanity(string text);
    
    public class DisplayName : ValueObject
    {
        public string Value { get; internal set; }

        protected DisplayName(){}
        internal DisplayName(string displayName)
        {
            Value = displayName;
        }

        public static DisplayName FromString (string displayName,CheckTextForProfanity hasProfanity)
        {
            if(string.IsNullOrWhiteSpace(displayName))
            {
                throw new ArgumentNullException(nameof(displayName));
            }
            if(hasProfanity(displayName))
            {
                throw new ProfanityFoundException(displayName);
            }
            return new DisplayName(displayName);
        }

        public static implicit operator string(DisplayName displayName) => displayName.Value;

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}