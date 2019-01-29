using Databind.Binding;
using Databind.Tests.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using System.Linq;
using System.IO;

namespace Databind.Tests
{
    public class InstanceTests
    {
        int dataCollectionSize = 1000000;

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

        [Fact]
        public void TestSpeedNormal()
        {
            var data = new List<SampleGenericModel>();
            for (int i = 0; i < dataCollectionSize; i++)
            {
                data.Add(new SampleGenericModel(Guid.NewGuid(), DateTime.Now));
            }

            var stopWatch = new Stopwatch();

            stopWatch.Reset();
            stopWatch.Start();
            var vals1 = data.Select(d => new {
                Id = d.Id,
                Time = d.Time
            }).ToList();
            stopWatch.Stop();
            Debug.WriteLine($"N: {stopWatch.ElapsedMilliseconds}");

        }

        [Fact]
        public void TestSpeedLambda()
        {
            var data = new List<SampleGenericModel>();
            for (int i = 0; i < dataCollectionSize; i++)
            {
                data.Add(new SampleGenericModel(Guid.NewGuid(), DateTime.Now));
            }

            var stopWatch = new Stopwatch();

            stopWatch.Reset();
            stopWatch.Start();
            var vals2 = data.Select(d => new
            {
                Id = Instance<SampleGenericModel>.Get(d, "Id"),
                Time = Instance<SampleGenericModel>.Get(d, "Time")
            }).ToList();
            stopWatch.Stop();
            Debug.WriteLine($"L: {stopWatch.ElapsedMilliseconds}");
        }

        [Fact]
        public void TestSpeedReflection()
        {
            var data = new List<SampleGenericModel>();
            for (int i = 0; i < dataCollectionSize; i++)
            {
                data.Add(new SampleGenericModel(Guid.NewGuid(), DateTime.Now));
            }

            var stopWatch = new Stopwatch();

            stopWatch.Reset();
            stopWatch.Start();
            var vals1 = data.Select(d => new
            {
                Id = d.GetType().GetProperty("Id").GetValue(d),
                Time = d.GetType().GetProperty("Time").GetValue(d)
            }).ToList();
            stopWatch.Stop();
            Debug.WriteLine($"R: {stopWatch.ElapsedMilliseconds}");

        }
    }
}
