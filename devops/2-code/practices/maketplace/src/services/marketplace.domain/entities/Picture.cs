using System.Security.Cryptography.X509Certificates;
using System;
using marketplace.domain.events;
using marketplace.domain.kernel;


namespace marketplace.domain.entities
{
    public class Picture : Entity
    {

        public Guid ParentId{ get; private set; }
        public PictureSize Size { get; private set; }
        public Uri Location { get; private set; }
        public int Order { get; private set; }

        protected Picture() { }

        public Picture(Guid parentId,Guid id,PictureSize size,Uri location ,int order)
        {
            ParentId = parentId;
            Id = new Guid(id.ToString());
            Location = location;
            Size = size;
            Order = order;
        }
        public Picture(Action<object> applier):base(applier)
        {

        }

        protected override void When(object @event)
        {
            switch(@event)
            {
                case PictureAdded e:
                    ParentId = e.ClassifiedAdId;
                    Id = new Guid(e.PictureId.ToString());
                    Location = new Uri(e.Url);
                    Size = new PictureSize(e.Width, e.Height);
                    Order = e.Order;
                    break;
                case PictureResized e:
                    Size = new PictureSize(e.Width, e.Height);
                    break;
            }
        }

        internal void Resize(PictureSize newSize) => 
            Apply(new PictureResized(Id, newSize.Height, newSize.Width));
    }
}