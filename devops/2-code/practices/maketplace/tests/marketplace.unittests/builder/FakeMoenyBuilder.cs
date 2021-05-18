using marketplace.domain.entities;

namespace marketplace.unittests.builder
{
    public class FakeMoneyBuilder
    {
        public static Money CreateTWD(decimal amount) => Money.Create(amount);
    }
}