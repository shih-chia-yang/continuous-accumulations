using System;

namespace marketplace.domain
{
    public class ClassifiedAd
    {
        public Guid Id { get; private set; }
        public Guid _ownerId;
        public string _title;
        public string _text;
        public string _price;

        public ClassifiedAd(Guid id)
        {
            if(id==default)
            {
                throw new ArgumentException("Identity must be specified", nameof(Id));
            }
            Id = id;
        }
    }
}
