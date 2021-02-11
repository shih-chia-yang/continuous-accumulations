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

        [Fact]
        public void IsMinus_InputIs2_ShouldBeReturn1()
        {
             var service = new TestService();
            int result = service.Minus(2);
            Assert.Equal(1, result);
        }
    }
}
