using System;
using marketplace.domain.entities;
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
            var decimalAmount = Money.Create(5);
            //When
            var stringAmount = Money.Create("5");
            //Then
            Assert.Equal(decimalAmount, stringAmount);
        }

        [Fact]
        [Trait("money","transfer_fail")]
        public void test_gives_invalid_string_should_be_throw_exception()
        {
            //Given
            Action createMoneyFromString = () =>Money.Create("adb");
            //When
            ArgumentException exception =Assert.Throws<ArgumentException>(createMoneyFromString);
            //Then
            Assert.Contains("invalid string cannot transfer to decimal", exception.Message);
        }

        [Fact]
        [Trait("money","outofrange")]
        public void test_gives_more_than_2_decimal_should_be_throw_exception()
        {
            //given
            decimal fakeAmount = 100.012M;
            Action outOfRange = () => Money.Create(fakeAmount);
            //when
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(outOfRange);

            Assert.Contains("Amount cannot have more than two decimal", exception.Message);
        }

        [Theory]
        [Trait("money","get_currency")]
        [InlineData("TWD","TWD")]
        [InlineData("USD","USD")]
        [InlineData("","TWD")]
        public void test_get_currency(string assignCurrency,string current)
        {
            //Given
            var twd = assignCurrency==string.Empty?Money.Create(5):Money.Create(5,assignCurrency);
            //When
            var currency = twd.Currency;
            //Then
            Assert.Equal(current, currency);
        }
        
        [Fact]
        [Trait("money","equality")]
        public void test_money_with_same_amount_should_equality()
        {
            var fiveNT = FakeMoneyBuilder.CreateTWD(5);
            var fivebucks = FakeMoneyBuilder.CreateUSD(5);


            Assert.Equal(fiveNT,FakeMoneyBuilder.CreateTWD(5));
            Assert.Equal(fivebucks, FakeMoneyBuilder.CreateUSD(5));
            Assert.NotEqual(fivebucks, fiveNT);
        }

        

        [Fact]
        [Trait("money","subtraction")]
        public void test_subtraction_of_money_gives_correct_amount()
        {
            //Given
            var profit = Money.Create(10);
            var cost = Money.Create(3);
            //When
            var earn = Money.Create(7);
            //Then
            Assert.Equal(profit - cost, earn);
        }
    }
}
