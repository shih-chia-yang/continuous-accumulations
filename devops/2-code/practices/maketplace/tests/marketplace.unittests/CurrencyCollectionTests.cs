using System.Linq;
using marketplace.domain.AggregateModels;
using marketplace.domain.services;
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
            collection.Add(twd);
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
            collection.Add(twd);
            //When
            var find = collection.Find(twd.CurrencyCode);
            //Then
            Assert.Equal(twd, find);
        }
    }
}