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
            var TWDtoUSD = new Pair("TWD", "USD");
            //When

            //Then
            Assert.Equal(TWDtoUSD, new Pair("TWD", "USD"));
        }

        [Theory]
        [Trait("pair","nullexception")]
        [InlineData("","")]
        [InlineData("source","")]
        [InlineData("","to")]
        public void test_null_value_should_throw_exception(string source,string to)
        {
            //Given
            Action nullValue=()=>new Pair(source, to);
            //When
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(nullValue);
            //Then
            Assert.NotNull(exception);
        }
    }
}