using System.Linq;
using marketplace.domain.entities;
using Xunit;

namespace marketplace.unittests
{
    public class CurrencyCollection_spec
    {
        [Fact]
        [Trait("currency","add")]
        public void test_add_currency_to_collection()
        {
            //Given
            var twd = Currency.Create("TWD", 2);
            ICurrencyLookup collection = new CurrencyCollection();
            //When
            collection.AddCurrency(twd);
            //Then
            Assert.True(collection.CurrencyList.Count() > 0);
        }

        [Fact]
        [Trait("currency","find")]
        public void test_find_currency()
        {
            //Given
            var twd = Currency.Create("TWD", 2);
            ICurrencyLookup collection = new CurrencyCollection();
            collection.AddCurrency(twd);
            //When
            var find = collection.FindCurrency(twd.CurrencyCode);
            //Then
            Assert.Equal(twd, find);
        }
    }
}