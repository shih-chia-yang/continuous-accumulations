using System;

namespace marketplace.domain.events.UserProfileEvents
{
    public class UserRegistered
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }

        public string DisplayName { get; set; }

        public UserRegistered(Guid userId,string fullName,string displayName)
        {
            UserId = userId;
            FullName = fullName;
            DisplayName = displayName;
        }
    }
}