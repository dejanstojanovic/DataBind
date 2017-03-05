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
    using (var dal = new DataAccess.DatabaseAccess(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
    {
        result = dal.ExecuteModels<Models.Customer>(
        "Customers_GetAll",
        new Dictionary<String, IConvertible= "" > {
            { "@DateRegistered",DateTime.Now }
        }).ToList();
    }
```
Where model Customer is decorated like following 

```csharp
namespace Models
public class Customer
{
    [DataBind("CustomerId")]
    public long ID { get; set; }

    [DataBind("First_Name")]
    public String FirstName { get; set; }

    [DataBind("Last_Name")]
    public String LastName { get; set; }
}
```

> **Note:**
Binding is done using reflection so it can hurt performance. I am planning to introduce binding meta caching so that binding will relly less on the reflection during the bind time
