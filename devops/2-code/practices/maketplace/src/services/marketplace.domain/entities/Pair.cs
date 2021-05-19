using System;
using System.Collections.Generic;
using marketplace.domain.kernal;

namespace marketplace.domain.entities
{
    public class Pair:ValueObject
    {
        public string Source { get;}

        public string  To { get;}

        public Pair(string source,string to)
        {
            if(string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException("source cannot be null or empty", nameof(source));
            }
            if (string.IsNullOrEmpty(to))
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