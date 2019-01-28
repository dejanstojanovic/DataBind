using System;
using System.Collections.Generic;
using System.Text;

namespace Databind.Tests.Models
{
    public class SampleGenericModel
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }

        public SampleGenericModel() { }

        public SampleGenericModel(Guid id, DateTime time)
        {
            this.Id = id;
            this.Time = time;
        }
             

    }
}
