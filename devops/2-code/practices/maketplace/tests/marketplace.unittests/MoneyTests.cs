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

        [Fact]
        [Trait("money","sum")]
        public void test_sum_of_money_gives_full_amount()
        {
            //Given
            var money1 = new Money(1);
            var money2 = new Money(2);
            var money3 = new Money(3);
            //When
            var banknote = new Money(6);
            //Then
            Assert.Equal(money1 + money2 + money3, banknote);
        }

        [Fact]
        [Trait("money","subtraction")]
        public void test_subtraction_of_money_gives_correct_amount()
        {
            //Given
            var profit = new Money(10);
            var cost = new Money(3);
            //When
            var earn = new Money(7);
            //Then
            Assert.Equal(profit - cost, earn);
        }
    }
}
