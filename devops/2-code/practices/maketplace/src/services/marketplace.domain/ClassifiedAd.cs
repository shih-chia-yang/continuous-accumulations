using System;

namespace marketplace.domain
{
    public class ClassifiedAd
    {
        public Guid Id { get; private set; }
        private UserId _ownerId;
        private string _title;
        private string _text;
        private Price _price;
        public ClassifiedAd(Guid id,UserId ownerId)
        {
            if(id==default)
            {
                throw new ArgumentException("Identity must be specified", nameof(Id));
            }
            Id = id;
            _ownerId=ownerId;
        }

        public void SetTitle(string title) => _title = title;

        public void UpdateText(string text) => _text = text;

        public void UpdatePrice(Price price) => _price = price;
    }
}
