using System.Linq;
using marketplace.domain.exceptions;

namespace marketplace.domain.entities
{
    public interface IExchangeService:IOperationExpression
    {

    }

    public class ExchangeService : IExchangeService
    {
        public ICurrencyExpression Sum(params ICurrencyExpression[] added)
        {
            if(added.Select(x=>x.Currency).Distinct().Count()>1)
            {
                throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
            }
            decimal sum = added.Select(x => x.Amount).Aggregate((cur, next) => cur + next);
            return Money.Create(sum);
        }
    }
}