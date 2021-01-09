using System;
using Xunit;

namespace testservice.unittests
{
    public class TestServiceTests
    {
        [Fact]
        public void IsAdd_InputIs1_ShouldBeReturn2()
        {
            var service = new TestService();
            int result = service.Add(1);
            Assert.Equal(2, result);
        }
    }
}
