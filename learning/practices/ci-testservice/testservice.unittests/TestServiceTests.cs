using System;
using Xunit;
//-----------------------------------------------------------------------
// <copyright file="TestService.cs" company="IP">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TestService.unittests
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
