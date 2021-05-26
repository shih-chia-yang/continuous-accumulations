using System;
using marketplace.domain.kernal;

namespace marketplace.domain.entities
{
    public class Picture : Entity
    {
        internal PictureSize Size { get; set; }
        internal Uri Location { get; set; }
        internal int Order { get; set; }

        public Picture(PictureSize size,Uri location,int order)
        {
            Size = size;
            Location = location;
            Order = order;
        }

        protected override void EnsureValidState()
        {
            throw new System.NotImplementedException();
        }

        protected override void When(object @event)
        {
            throw new System.NotImplementedException();
        }
    }
}