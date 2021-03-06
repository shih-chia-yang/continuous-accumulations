using System;
using marketplace.domain.AggregateModels;
using marketplace.domain.exceptions;
using marketplace.unittests.builder;
using Xunit;

namespace marketplace.unittests
{
    public class MoneyTests
    {
        [Fact]
        [Trait("money","transfer")]
        public void test_gives_string_should_be_transfer_amount()
        {           
            //Given
            var decimalAmount = Money.Create(5,Currency.Default);
            //When
            var stringAmount = Money.Create("5.00",Currency.Default);
            //Then
            Assert.Equal(decimalAmount, stringAmount);
        }

        [Fact]
        [Trait("money","transfer_fail")]
        public void test_gives_invalid_currency_should_be_throw_exception()
        {
            //Given
            Action createMoneyFromString = () =>Money.Create("adb",Currency.Default);
            //When
            ArgumentException exception =Assert.Throws<ArgumentException>(createMoneyFromString);
            //Then
            Assert.Contains("invalid string cannot transfer to decimal", exception.Message);
        }

        [Fact]
        [Trait("money","useless")]
        public void test_useless_currency_should_not_be_allowed()
        {
            //Given
            var useless= Currency.Create("DEM",2,false);
            //When
            Assert.Throws<ArgumentException>(() => Money.Create(2, useless));
            //Then
        }

        [Fact]
        [Trait("money","outofrange")]
        public void test_throw_when_too_many_decimal_places()
        {
            //given
            decimal fakeAmount = 100.012M;
            Action outOfRange = () => Money.Create(fakeAmount,Currency.Default);
            //when
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(outOfRange);
            string errorMessage = $"Amount in {Currency.Default.CurrencyCode} cannot have more than {Currency.Default.DecimalPlace} decimals";
            //then
            Assert.Contains(errorMessage,exception.Message);
        }

        [Theory]
        [Trait("money","get_currency")]
        [InlineData("TWD",2)]
        [InlineData("USD",2)]
        [InlineData("",2)]
        public void test_get_currency(string assignCurrency,int decimalPlace)
        {
            //Given
            var fakeCurrency = Currency.Create(assignCurrency, decimalPlace);
            var money = assignCurrency==string.Empty?Money.Create(5):Money.Create(5,fakeCurrency);
            //When
            var currency = money.Currency;
            //Then
            Assert.Equal(fakeCurrency, currency);
        }
        
        [Fact]
        [Trait("money","equality")]
        public void test_money_with_same_amount_should_equality()
        {
            //given
            var fiveNT = FakeMoneyBuilder.CreateTWD(5);
            var fivebucks = FakeMoneyBuilder.CreateUSD(5);
            //when
            //then
            Assert.Equal(fiveNT,FakeMoneyBuilder.CreateTWD(5));
            Assert.Equal(fivebucks, FakeMoneyBuilder.CreateUSD(5));
            Assert.NotEqual(fivebucks, fiveNT);
        }
        
    }
}
