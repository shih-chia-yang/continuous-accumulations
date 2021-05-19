using System.Collections;
using System.Linq;
using marketplace.domain.exceptions;

namespace marketplace.domain.entities
{
    public interface IExchangeService:IOperationExpression
    {
        decimal GetRate(Pair targetPair);
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
            return Money.Create(currency.Amount/GetRate(exchangePair),to);
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

        public ICurrencyExpression Sum(params ICurrencyExpression[] added)
        {
            if(added.Select(x=>x.Currency).Distinct().Count()>1)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }
            decimal sum = added.Select(x => x.Amount).Aggregate((cur, next) => cur + next);
            return Money.Create(sum);
        }

        public void AddRate(Pair pair, decimal rate)
        {
            _rates.Add(pair, rate);
        }

        public decimal GetRate(Pair targetPair)
        {
            var rate = (decimal)RateList[targetPair];
            return rate;
        }
    }
}