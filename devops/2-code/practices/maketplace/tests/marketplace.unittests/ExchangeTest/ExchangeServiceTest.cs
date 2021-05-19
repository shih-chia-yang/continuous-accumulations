using System;
using marketplace.domain.entities;
using marketplace.domain.exceptions;
using marketplace.unittests.builder;
using Xunit;

namespace marketplace.unittests.ExchangeTest
{
    public class ExchangeServiceTest
    {
        private readonly IExchangeService exchange;
        public ExchangeServiceTest()
        {
            exchange = new ExchangeService();
        }

        [Fact]
        [Trait("exchange","sum")]
        public void test_sum_of_money_gives_full_amount()
        {
            //Given
            var money1 = Money.Create(1);
            var money2 = Money.Create(2);
            var money3 = Money.Create(3);
            //When
            var sum = exchange.Sum(money1, money2, money3);
            var banknote = Money.Create(6);
            //Then
            Assert.Equal(sum, banknote);
        }

        [Fact]
        [Trait("exchange","sum_of_different")]
        public void test_sum_of_different_currency_should_be_throw_exception()
        {
            //Given
            var usd = FakeMoneyBuilder.CreateUSD(5);
            //When
            Action sumDifferent =()=> exchange.Sum(FakeMoneyBuilder.CreateTWD(5),usd);
            var mismatch = Assert.Throws<CurrencyMismatchException>(sumDifferent);
            //Then
            Assert.Contains("Cannot subtract amounts with different currencies", mismatch.Message);
        }

        [Fact]
        [Trait("money","subtraction")]
        public void test_subtraction_of_money_gives_correct_amount()
        {
            //Given
            var profit = FakeMoneyBuilder.CreateTWD(10);
            var cost = FakeMoneyBuilder.CreateTWD(3);
            var except_earn = FakeMoneyBuilder.CreateTWD(7);
            //When
            var actual_earn = exchange.Subtraction(profit, cost);
            //Then
            Assert.Equal(except_earn,actual_earn);
        }
    }
}