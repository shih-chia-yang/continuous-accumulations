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