using System.Net.Mime;
using System.Security.Cryptography;
using System;
using marketplace.domain.entities;
using marketplace.domain.events;
using marketplace.domain.kernal;
using marketplace.domain.Validation;

namespace marketplace.domain
{
    public enum ClassifiedState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    }
    public class ClassifiedAd:Entity
    {
        public Guid Id { get; private set; }
        public UserId OwnerId { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Price Price { get; private set; }

        public ClassifiedState State { get; private set;}

        public UserId ApprovedBy{ get; private set;}
        public ClassifiedAd(Guid id,UserId ownerId)=>Apply(new ClassifiedAdCreated(id,ownerId));

        public void SetTitle(ClassifiedAdTitle title)=>
            Apply(new ClassifiedAdTitleChanged(Id, title.Value));

        public void UpdateText(ClassifiedAdText text)=>
            Apply(new ClassifiedAdTextUpdated(Id, text.Value));

        public void UpdatePrice(Price price)=>
            Apply(new ClassifiedAdPriceUpdated(Id, price.Amount, price.Currency.CurrencyCode));

        public void RequestToPublish()=>
            Apply(new ClassifiedAdSentToReview(Id));

        protected override void When(object @event)
        {
            switch(@event)
            {
                case ClassifiedAdCreated e:
                    Id = e.Id;
                    OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedState.Inactive;
                    break;
                case ClassifiedAdTitleChanged e:
                    Title = ClassifiedAdTitle.FromString(e.Title);
                    break;
                case ClassifiedAdTextUpdated e:
                    Text = ClassifiedAdText.FromString(e.Text);
                    break;
                case ClassifiedAdPriceUpdated e:
                    Price = Price.Create(e.Price,Currency.Create(e.CurrencyCode,2));
                    break;
                case ClassifiedAdSentToReview e:
                    State = ClassifiedState.PendingReview;
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            var valid = new ClassifiedAdValidation(this);
            valid.Validate();
        }
    }
}
