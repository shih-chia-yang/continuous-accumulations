using System;

namespace marketplace.domain.events.UserProfileEvents
{
    public class ProfilePhotoUploaded
    {
        public Guid UserId { get; set; }
        public string PhotoUrl { get; set; }

        public ProfilePhotoUploaded(Guid userId,string url)
        {
            UserId = userId;
            PhotoUrl = url;
        }
    }
}