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
        public UserId OwnerId { get;}
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Price Price { get; private set; }

        public ClassifiedState State { get; private set;}

        public UserId ApprovedBy{ get; private set;}
        public ClassifiedAd(Guid id,UserId ownerId)
        {
            if(id==default)
            {
                throw new ArgumentException("Identity must be specified", nameof(Id));
            }
            Id = id;
            OwnerId=ownerId;
            State = ClassifiedState.Inactive;
            Raise(new ClassifiedAdCreated(Id,ownerId));
        }

        public void SetTitle(ClassifiedAdTitle title)
        {
            Title = title;
            Raise(new ClassifiedAdTitleChanged(Id, title.Value));
        }

        public void UpdateText(ClassifiedAdText text)
        {
            Text = text;
            Raise(new ClassifiedAdTextUpdated(Id, text.Value));
        }

        public void UpdatePrice(Price price)
        {
            Price = price;
            Raise(new ClassifiedAdPriceUpdated(Id, price.Amount, price.Currency.CurrencyCode));
        } 

        public void RequestToPublish()
        {
            this.State = ClassifiedState.PendingReview;
            var valid = new ClassifiedAdValidation(this);
            valid.Validate();
            Raise(new ClassifiedAdSentToReview(Id));
        }
    }
}
