using Databind.Binding;
using Databind.Tests.Models;
using System;
using Xunit;

namespace Databind.Tests
{
    public class InstanceTests
    {
        [Fact]
        public void TestPropertyGet()
        {
            var obj = new SampleGenericModel(Guid.NewGuid(), DateTime.Now);
            var id = Instance<SampleGenericModel>.Get(obj, "Id");

            Assert.IsAssignableFrom<Guid>(id);
            Assert.NotNull(id);
            Assert.False((Guid)id == Guid.Empty);
        }
    }
}
