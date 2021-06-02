using System;

namespace marketplace.domain.events.UserProfileEvents
{
    public class UserDisplayNameUpdated
    {
        public Guid UserId { get; set; }

        public string DisplayName { get; set; }

        public UserDisplayNameUpdated(Guid userId,string displayName)
        {
            UserId = userId;
            DisplayName = displayName;
        }
    }
}