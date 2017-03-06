using DataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DataProvider.Attributes;

namespace Common.DataProvider.SampleApp.Models
{
    [ModelBind(true,StringComparison.InvariantCultureIgnoreCase)]
    public class Order
    {
        [PropertyBind("OrderID")]
        public int ID { get; set; }

        [PropertyBind("OrderDate")]
        public DateTime DateOrdered { get; set; }

        [PropertyBind("ShipName")]
        public String Name { get; set; }

        public String ShipCity { get; set; }
    }
}
