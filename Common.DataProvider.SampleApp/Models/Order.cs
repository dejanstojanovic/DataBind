using DataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataProvider.Attributes;

namespace Common.DataProvider.SampleApp.Models
{
    public class Order
    {
        [DataBind("OrderID")]
        public int ID { get; set; }

        [DataBind("OrderDate")]
        public DateTime DateOrdered { get; set; }

        [DataBind("ShipName")]
        public String Name { get; set; }
    }
}
