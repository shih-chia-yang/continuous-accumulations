using System;
using marketplace.domain.kernel;
using marketplace.domain.Validation;
using System.Collections.Generic;
using System.Linq;
using marketplace.domain.events.ClassifiedAdEvents;

namespace marketplace.domain.AggregateModels.ClassifiedAdAggregate
{
    public enum ClassifiedAdState
    {
        PendingReview=1,
        Active=2,
        Inactive=3,
        MarkedAsSold=4
    }
    public class ClassifiedAd:AggregateRoot
    {
        public Guid ClassifiedAdId{ get; private set; }
        public UserId OwnerId { get; private set; }
        public ClassifiedAdTitle Title { get; private set; }
        public ClassifiedAdText Text { get; private set; }
        public Price Price { get; private set; }

        private List<Picture> _pictures;
        public IEnumerable<Picture> Pictures => _pictures.AsReadOnly();

        public ClassifiedAdState State { get; private set;}
        public UserId ApprovedBy{ get; private set;}

        protected ClassifiedAd(){_pictures = new List<Picture>();}
        public ClassifiedAd(Guid id,UserId ownerId):this()
        {
            Apply(new ClassifiedAdCreated(id,ownerId));
        }

        public void SetTitle(ClassifiedAdTitle title)=>
            Apply(new ClassifiedAdTitleChanged(Id, title.Value));

        public void UpdateText(ClassifiedAdText text)=>
            Apply(new ClassifiedAdTextUpdated(Id, text.Value));

        public void UpdatePrice(Price price)=>
            Apply(new ClassifiedAdPriceUpdated(Id, price.Amount, price.Currency.CurrencyCode));

        public void RequestToPublish()=>
            Apply(new ClassifiedAdSentToReview(Id));

        public void Publish(UserId userId)
            => Apply(new ClassifiedAdPublished(Id = Id, ApprovedBy = userId));

        public Picture FindPicture(Guid id) => Pictures.FirstOrDefault(x => x.Id == id);
        public void AddPicture(Uri pictureUrl, PictureSize size) => 
            Apply(new PictureAdded(
                Id,Guid.NewGuid(),
                pictureUrl.ToString(),
                size.Height,size.Width,
                _pictures.Count==0?1:Pictures.Max(x=>x.Order)
                ));

        public void ResizePicture(Guid id, PictureSize newSize) =>
            Apply(new PictureResized(id, newSize.Width, newSize.Height));
        // {
        //     var picture = FindPicture(id);
        //     if(picture==null)
        //         throw new InvalidOperationException("Cannot resize a picture that picture not found");
        //     picture.Resize(newSize);
        // }
        protected override void When(object @event)
        {
            switch(@event)
            {
                case ClassifiedAdCreated e:
                    Id = e.Id;
                    OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedAdState.Inactive;
                    Title = ClassifiedAdTitle.NoTitle();
                    Text = ClassifiedAdText.NoText();
                    Price = Price.NoPrice();
                    ApprovedBy = UserId.NoUser();
                    ClassifiedAdId = e.Id;
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
                    State = ClassifiedAdState.PendingReview;
                    break;
                case ClassifiedAdPublished e:
                    ApprovedBy = new UserId(e.ApprovedBy);
                    State = ClassifiedAdState.Active;
                    break;
                case PictureAdded e:
                    var newPicture = new Picture(Apply);
                    ApplyToEntity(newPicture, e);
                    _pictures.Add(newPicture);
                    break;
                case PictureResized e:
                    var picture = FindPicture(e.PictureId);
                    ApplyToEntity(picture, @event);
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
