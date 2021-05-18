namespace marketplace.domain.entities
{
    public interface IOperationExpression
    {
        ICurrencyExpression Sum(params ICurrencyExpression[] added);
    }
}