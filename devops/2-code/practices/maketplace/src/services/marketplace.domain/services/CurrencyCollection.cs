using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using marketplace.domain.entities;

namespace marketplace.domain.services
{
    public interface ICurrencyLookup
    {
        IEnumerable<Currency> CurrencyList{ get; }
        Currency Find(string currencyCode);

        void Add(Currency currency);
    }


    public class CurrencyCollection : ICurrencyLookup
    {
        public IEnumerable<Currency> CurrencyList => _currencies;
        private List<Currency> _currencies;

        public CurrencyCollection()
        {
            _currencies = new List<Currency>();
        }

        public void Add(Currency currency)
        {
            if(Find(currency.CurrencyCode)!=null)
            {
                throw new ArgumentException("CurrencyCode doesn't exist", nameof(currency));
            }
            _currencies.Add(currency);
        }

        public Currency Find(string currencyCode)
        {
            return _currencies.Where(x => x.CurrencyCode == currencyCode).FirstOrDefault();
        }
    }
}