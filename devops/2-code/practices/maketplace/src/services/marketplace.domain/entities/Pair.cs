using System;
using System.Collections.Generic;
using marketplace.domain.kernal;

namespace marketplace.domain.entities
{
    public class Pair:ValueObject
    {
        public Currency Source { get;}

        public Currency  To { get;}

        public Pair(Currency source,Currency to)
        {
            if(!source.InUse)
            {
                throw new ArgumentNullException("source cannot be null or empty", nameof(source));
            }
            if(!to.InUse)
            {
                throw new ArgumentNullException("to cannot be null or empty", nameof(to));
            }
            Source = source;
            To = to;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Source;
            yield return To;
        }
    }
}