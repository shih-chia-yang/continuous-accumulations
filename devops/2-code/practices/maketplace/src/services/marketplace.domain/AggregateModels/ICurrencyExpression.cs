using marketplace.domain.services;

namespace marketplace.domain.AggregateModels
{
    public interface ICurrencyExpression
    {
        decimal Amount{ get; }
        Currency Currency { get; }

        // ICurrencyExpression Create(decimal amount);
    }
}