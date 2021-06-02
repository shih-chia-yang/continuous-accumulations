using System;
using marketplace.domain.events.UserProfileEvents;
using marketplace.domain.kernel;

namespace marketplace.domain.AggregateModels.UserAggregate
{
    public class UserProfile:AggregateRoot
    {
        public FullName FullName { get; private set; }
        public DisplayName DisplayName{ get; private set; }

        public string PhotoUrl { get; private set; }

        protected UserProfile() { }
        public UserProfile(UserId id, FullName fullName, DisplayName displayName) =>
            Apply(new UserRegistered(id, fullName, displayName));

        public void UpdateFullName(FullName fullName) =>
            Apply(new UserFullNameUpdated(Id, fullName));

        public void UpdateDisplayName(DisplayName displayName) =>
            Apply(new UserDisplayNameUpdated(Id, displayName));

        public void UpdateProfilePhoto(Uri photoUrl) =>
            Apply(new ProfilePhotoUploaded(Id, photoUrl.ToString()));
        protected override void When(object @event)
        {
            switch(@event)
            {
                case UserRegistered e :
                    Id = new UserId(e.UserId);
                    FullName = FullName.FromString(e.FullName);
                    DisplayName = new DisplayName(e.DisplayName);
                    break;
                case UserFullNameUpdated e :
                    FullName = FullName.FromString(e.FullName);
                    break;
                case UserDisplayNameUpdated e :
                    DisplayName = new DisplayName(e.DisplayName);
                    break;
                case ProfilePhotoUploaded e :
                    PhotoUrl = e.PhotoUrl;
                    break;
            }
        }
        protected override void EnsureValidState()
        {
            
        }

    }
}