using System;
namespace marketplace.domain.exceptions
{
    public class CurrencyMismatchException:Exception
    {
        public CurrencyMismatchException(string message):base(message)
        {
            
        }
    }
}