namespace marketplace.domain.entities
{
    public interface IOperationExpression
    {
        ICurrencyExpression Sum(string to,params ICurrencyExpression[] added);

        ICurrencyExpression Subtraction(params ICurrencyExpression[] minuend);

        ICurrencyExpression Times(ICurrencyExpression currency, decimal multiplier);

        ICurrencyExpression ExchangeTo(ICurrencyExpression currency, string to);
    }
}