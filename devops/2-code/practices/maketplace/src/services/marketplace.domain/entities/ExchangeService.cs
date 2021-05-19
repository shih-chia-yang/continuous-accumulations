using System;
using System.Collections;
using System.Linq;
using marketplace.domain.exceptions;

namespace marketplace.domain.entities
{
    public interface IExchangeService:IOperationExpression
    {
        Hashtable RateList{ get; }
        decimal GetRate(string source,string to);
        void AddRate(Pair pair, decimal rate);
    }

    public class ExchangeService : IExchangeService
    {
        public Hashtable RateList => _rates;
        private Hashtable _rates;

        public ExchangeService()
        {
            _rates = new Hashtable();
        }

        
        public ICurrencyExpression ExchangeTo(ICurrencyExpression currency, string to)
        {
            var exchangePair = new Pair(currency.Currency, to);
            return Money.Create(currency.Amount/GetRate(currency.Currency,to),to);
        }

        public ICurrencyExpression Subtraction(params ICurrencyExpression[] minuend)
        {
            if(minuend.Select(x=>x.Currency).Distinct().Count()>1)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }
            decimal result = minuend.Select(x => x.Amount).Aggregate((cur, next) => cur - next);
            return Money.Create(result);
        }

        public ICurrencyExpression Sum(string to,params ICurrencyExpression[] added)
        {
            decimal sum = added.Select(x =>ExchangeTo(x,to).Amount).Aggregate((cur, next) => cur + next);
            return Money.Create(sum,to);
        }

        public void AddRate(Pair pair, decimal rate)
        {
            if(RateList.Contains(pair))
            {
                throw new ArgumentException("Pair and exchange rate already existed", nameof(pair));
            }
            _rates.Add(pair, rate);
        }

        public decimal GetRate(string source,string to)
        {
            if(source.Equals(to))return 1;
            var targetPair = new Pair(source, to);
            var rate = (decimal)RateList[targetPair];
            return rate;
        }
    }
}