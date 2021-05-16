using System;
using marketplace.domain;
using Xunit;

namespace marketplace.unittests
{
    public class MoneyTests
    {
        [Fact]
        [Trait("money","equality")]
        public void test_money_with_same_amount_should_equality()
        {
            var firstAmount = new Money(5);
            var secondAmount = new Money(5);


            Assert.Equal(firstAmount, secondAmount);
        }
    }
}
