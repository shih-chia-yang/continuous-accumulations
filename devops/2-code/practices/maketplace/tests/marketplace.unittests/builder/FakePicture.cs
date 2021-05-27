using System;
using marketplace.domain.entities;

namespace marketplace.unittests.builder
{
    public class FakePicture :Picture
    {
        public FakePicture(Action<object> applier) : base(applier)
        {
        }

        public void Handle(object @event) 
        {
            base.When(@event);
        }   
    }
}