namespace marketplace.domain.entities
{
    public interface IOperationExpression
    {
        ICurrencyExpression Sum(params ICurrencyExpression[] added);

        ICurrencyExpression Subtraction(params ICurrencyExpression[] minuend);

        ICurrencyExpression ExchangeTo(ICurrencyExpression currency, string to);
    }
}