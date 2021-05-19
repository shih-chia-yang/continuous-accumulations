using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace marketplace.domain.entities
{
    public interface ICurrencyLookup
    {
        IEnumerable<Currency> CurrencyList{ get; }
        Currency FindCurrency(string currencyCode);

        void AddCurrency(Currency currency);
    }


    public class CurrencyCollection : ICurrencyLookup
    {
        public IEnumerable<Currency> CurrencyList => _currencies;
        private List<Currency> _currencies;

        public CurrencyCollection()
        {
            _currencies = new List<Currency>();
        }

        public void AddCurrency(Currency currency)
        {
            if(FindCurrency(currency.CurrencyCode)!=null)
            {
                throw new ArgumentException("CurrencyCode doesn't exist", nameof(currency));
            }
            _currencies.Add(currency);
        }

        public Currency FindCurrency(string currencyCode)
        {
            return _currencies.Where(x => x.CurrencyCode == currencyCode).FirstOrDefault();
        }
    }
}