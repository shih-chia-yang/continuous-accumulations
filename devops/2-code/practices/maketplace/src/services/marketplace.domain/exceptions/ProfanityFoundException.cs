using System;
namespace marketplace.domain.exceptions
{
    public class ProfanityFoundException:Exception
    {
        public ProfanityFoundException(string text):base($"Profanity found in text:{text}")
        {
        
        }
    }
}