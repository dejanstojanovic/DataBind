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
            obj.Number = 33;

            var id = Instance<SampleGenericModel>.Get(obj, "Id");

            Instance<SampleGenericModel>.Set(obj, "Number", 12);
            var number = Instance<SampleGenericModel>.Get(obj, "Number");


            Assert.IsAssignableFrom<Guid>(id);
            Assert.NotNull(id);
            Assert.False((Guid)id == Guid.Empty);

            Assert.IsAssignableFrom<int>(number);
            Assert.True((int)number == 12);

        }
    }
}
