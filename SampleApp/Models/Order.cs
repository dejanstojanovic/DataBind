using DataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Models
{
    public class Order
    {
        [DataBind("OrderID")]
        public long ID { get; set; }

        [DataBind("OrderDate")]
        public DateTime DateOrdered { get; set; }

        [DataBind("ShipName")]
        public String Name { get; set; }
    }
}
