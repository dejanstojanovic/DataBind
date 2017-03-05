# DataBind
Simple data binding class for easy binding of
 - DataRow to single POCO class
 - DataTable to single POCO class
 - DataTable to multiple POCO classes
 - DataSet to single POCO class
 - DataSet to multiple POCO classes
 - SqlDataReader to single POCO class
 - SqlDataReader to multiple POCO classes
 - One POCO class to another POCO class
 - ADO objects to dynamic object instance

Sample

```csharp
using DataBinding;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Diagnostics;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            using (var dal = new DatabaseAccess(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteModels<Models.Customer>(
                "Customers_GetAll",
                new Dictionary<String, IConvertible> {
                    { "@Country",null }
                }).ToList();

            }

            timer.Stop();

            Console.WriteLine("Miliseonds: {0}",timer.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}

```
Where model Customer is decorated like following 

```csharp
using System;
using DataBinding;

namespace SampleApp.Models
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

```

> **Note:**
Binding is done using reflection so it can hurt performance. I am planning to introduce binding meta caching so that binding will relly less on the reflection during the bind time
