namespace marketplace.domain.entities
{
    public interface ICurrencyExpression
    {
        decimal Amount{ get; }
        Currency Currency { get; }

        // ICurrencyExpression Create(decimal amount);
    }
}