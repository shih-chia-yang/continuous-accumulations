using System;
using marketplace.domain.entities;
using Xunit;

namespace marketplace.unittests.ExchangeTest
{
    public class PairTests
    {
        [Fact]
        [Trait("pair","equality")]
        public void test_pair_equality()
        {
            //Given
            var twd= Currency.Default;
            var usd = Currency.Create("USD", 2);
            //When
            var TWDtoUSD = new Pair(Currency.Default, usd);
            //Then
            Assert.Equal(TWDtoUSD, new Pair(twd, usd));
        }

        [Fact]
        [Trait("pair","nullexception")]
        public void test_null_value_should_throw_exception()
        {
            //Given
            
            Action nullValue=()=>new Pair(Currency.None, Currency.None);
            //When
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(nullValue);
            //Then
            Assert.NotNull(exception);
        }
    }
}