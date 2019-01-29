using Databind.Binding.Attributes;
using System;

namespace Common.DataProvider.SampleApp.Models
{
   public class Customer
    {
        [PropertyBind("CustomerID")]
        public String ID { get; set; }

        [PropertyBind("CompanyName")]
        public String Company { get; set; }

        [PropertyBind("ContactName")]
        public String ContactPerson { get; set; }

        [PropertyBind("Address")]
        public String Address { get; set; }
    }
}
