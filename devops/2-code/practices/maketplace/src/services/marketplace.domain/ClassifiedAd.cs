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

    }
}
