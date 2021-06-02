using marketplace.domain.AggregateModels;

namespace marketplace.domain.services
{
    public interface IOperationExpression
    {
        ICurrencyExpression Sum(Currency to,params ICurrencyExpression[] added);

        ICurrencyExpression Subtraction(params ICurrencyExpression[] minuend);

        ICurrencyExpression Times(ICurrencyExpression currency, decimal multiplier);

        ICurrencyExpression ExchangeTo(ICurrencyExpression currency, Currency to);
    }
}