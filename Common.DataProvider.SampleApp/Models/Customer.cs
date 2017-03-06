using System;
using DataBinding;
using Common.DataProvider;
using Common.DataProvider.Attributes;

namespace Common.DataProvider.SampleApp.Models
{
   public class Customer
    {
        [DataBind("CustomerID")]
        public String ID { get; set; }

        [DataBind("CompanyName")]
        public String Company { get; set; }

        [DataBind("ContactName")]
        public String ContactPerson { get; set; }

        [DataBind("Address")]
        public String Address { get; set; }
    }
}
