namespace marketplace.domain.entities
{
    public interface ICurrencyExpression
    {
        decimal Amount{ get; }
        string Currency { get; }
    }
}