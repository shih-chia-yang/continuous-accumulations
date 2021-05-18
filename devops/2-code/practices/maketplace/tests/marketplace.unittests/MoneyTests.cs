using System;
using marketplace.domain.entities;
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
        
        [Fact]
        [Trait("money","equality")]
        public void test_money_with_same_amount_should_equality()
        {
            var firstAmount = Money.Create(5);
            var secondAmount = Money.Create(5);


            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        [Trait("money","sum")]
        public void test_sum_of_money_gives_full_amount()
        {
            //Given
            var money1 = Money.Create(1);
            var money2 = Money.Create(2);
            var money3 = Money.Create(3);
            //When
            var banknote = Money.Create(6);
            //Then
            Assert.Equal(money1 + money2 + money3, banknote);
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
