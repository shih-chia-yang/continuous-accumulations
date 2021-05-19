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
    }
}