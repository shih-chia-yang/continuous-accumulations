using System;

namespace marketplace.domain
{
    public class UserId
    {
        private readonly Guid _value;

        public UserId(Guid value)
        {
            if(value==default)
            {
                throw new ArgumentException("Owner id must be specified", nameof(value));
            }
            _value = value;
        }
    }
}