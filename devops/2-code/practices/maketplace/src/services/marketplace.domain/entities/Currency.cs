using System.Collections.Generic;
using marketplace.domain.kernal;

namespace marketplace.domain.entities
{
    public class Currency : ValueObject
    {
        public string CurrencyCode { get; set; }

        public bool InUse { get; set; }

        public int DecimalPlace { get; set; }

        public static Currency None = new Currency { InUse = false};

        public static Currency Default = new Currency("TWD", 2);

        public Currency()
        {
            
        }

        public static Currency Create(string code,int decimalPlace,bool inUse=true)
        {
            if(string.IsNullOrEmpty(code))
                return Currency.Default;
            return new Currency(code, decimalPlace, inUse);
        }

        protected Currency(string code,int decimalPlace,bool inUse=true)
        {
            CurrencyCode = code;
            DecimalPlace = decimalPlace;
            InUse = inUse;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CurrencyCode;
            yield return InUse;
            yield return DecimalPlace;
        }
    }
}