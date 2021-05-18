using marketplace.domain.entities;

namespace marketplace.unittests.builder
{
    public class FakeMoneyBuilder
    {
        public static Money CreateTWD(decimal amount) => Money.Create(amount);

        public static Money CreateUSD(decimal amount) => Money.Create(amount,"USD");

        public static Money CreateYEN(decimal amount) => Money.Create(amount,"YEN");
    }
}