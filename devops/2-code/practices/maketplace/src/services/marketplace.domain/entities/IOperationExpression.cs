namespace marketplace.domain.entities
{
    public interface IOperationExpression
    {
        ICurrencyExpression Sum(string to,params ICurrencyExpression[] added);

        ICurrencyExpression Subtraction(params ICurrencyExpression[] minuend);

        ICurrencyExpression ExchangeTo(ICurrencyExpression currency, string to);
    }
}