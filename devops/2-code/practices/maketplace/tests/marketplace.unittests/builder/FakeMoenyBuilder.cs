using marketplace.domain.entities;

namespace marketplace.unittests.builder
{
    public class FakeMoneyBuilder
    {
        public static Money CreateTWD(decimal amount) => Money.Create(amount,Currency.Default);

        public static Money CreateUSD(decimal amount) => Money.Create(amount,Currency.Create("USD",2));

        public static Money CreateYEN(decimal amount) => Money.Create(amount,Currency.Create("YEN",0));
    }
}