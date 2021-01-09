using System;
using Xunit;

namespace testservice.unittests
{
    public class UnitTest1
    {
        [Fact]
        public void IsAdd_InputIs1_ShouldBeReturn2()
        {
            var service = new testservice();
            int result = service.Add(1);
            Assert.Equal(2, result);
        }
    }
}
