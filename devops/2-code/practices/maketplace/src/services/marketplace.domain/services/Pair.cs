using System;
using System.Collections.Generic;
using marketplace.domain.AggregateModels;
using marketplace.domain.kernel;
namespace marketplace.domain.services
{
    public class Pair:ValueObject
    {
        public Currency Source { get;}

        public Currency  To { get;}

        public Pair(Currency source,Currency to)
        {
            if(!source.InUse)
            {
                throw new ArgumentNullException($"source Currency {source} is not valid", nameof(source));
            }
            if(!to.InUse)
            {
                throw new ArgumentNullException($"to Currency {to} is not valid", nameof(to));
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