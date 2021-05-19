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
            Pair TWDtoUSD = new Pair("TWD", "USD");
            Pair USDtoTWD = new Pair("USD","TWD");
            exchange.AddRate(TWDtoUSD,5);
            exchange.AddRate(USDtoTWD,0.2M);
        }

        [Fact]
        [Trait("exchange","exchange_currency")]
        public void test_currency_exchange_to_another_currency()
        {
            //Given
            
            var fiveTWD = FakeMoneyBuilder.CreateTWD(5);
            //When
            var usd=exchange.ExchangeTo(fiveTWD,"USD");
            //Then
            Assert.True(usd.Currency.Equals("USD"));
            Assert.Equal(1, usd.Amount);
        }

        [Fact]
        [Trait("exchange","addrate")]
        public void test_add_rate_than_list_should_be_added()
        {
            //Given
            var fakePair = new Pair("test1", "test2");
            var originCount = exchange.RateList.Count;
            //When
            exchange.AddRate(fakePair, 2);
            //Then
            Assert.Equal(originCount + 1, exchange.RateList.Count);
        }

        [Fact]
        [Trait("exchange","add_twice_pair")]
        public void test_add_same_pair_and_value_should_be_throw_exception()
        {
            //Given
            var fakePair = new Pair("test1", "test2");
            exchange.AddRate(fakePair,3);
            //When
            Action addtwice=()=>exchange.AddRate(fakePair,3);
            ArgumentException exception = Assert.Throws<ArgumentException>(addtwice);
            //Then
            Assert.Contains("Pair and exchange rate already existed", exception.Message);
        }

        [Fact]
        [Trait("exchange","getrate")]
        public void test_get_rate()
        {
            //Given
            var fakePair = new Pair("test1", "test2");
            exchange.AddRate(fakePair, 2);
            //When

            //Then
            Assert.Equal(2, exchange.GetRate(fakePair));
        }

        [Fact]
        [Trait("exchange","sum")]
        public void test_sum_of_money_gives_full_amount()
        {
            //Given

            var money1 = FakeMoneyBuilder.CreateTWD(5);
            var money2 = FakeMoneyBuilder.CreateUSD(10);
            var money3 = FakeMoneyBuilder.CreateTWD(3);
            //When
            var sum = exchange.Sum("TWD",money1, money2, money3);
            var banknote = Money.Create(58);
            //Then
            Assert.Equal(sum, banknote);
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