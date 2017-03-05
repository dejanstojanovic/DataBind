using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.Sample.Models
{
   public class Customer
    {
        [DataAccess.DataBind("CustomerId")]
        public long ID { get; set; }

        [DataAccess.DataBind("First_Name")]
        public String FirstName { get; set; }

        [DataAccess.DataBind("Last_Name")]
        public String LastName { get; set; }
    }
}
