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

### Sample

> **Note:**
For this sample I used Northwind database provided by Microsoft. You can download it from https://northwinddatabase.codeplex.com/

```csharp
 using (var dal = new DatabaseAccess(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteModels<Order>(
                "Orders_GetAll",
                new Dictionary<String, IConvertible> {
                    { "@Country",null }
                }).ToList();

            }
```
Where model Customer is decorated like following 

```csharp
    public class Order
    {
        [DataBind("OrderID")]
        public int ID { get; set; }

        [DataBind("OrderDate")]
        public DateTime DateOrdered { get; set; }

        [DataBind("ShipName")]
        public String Name { get; set; }
    }
```

Sample stored procedure
```sql
CREATE PROCEDURE Orders_GetAll 
@Country VARCHAR(100) = NULL

AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM  dbo.Orders
	UNION
	SELECT * FROM dbo.Orders WHERE ShipCountry = ISNULL(@Country,ShipCountry)
END
GO

```

> **Note:**
Binding is done using reflection so it can hurt performance. I am planning to introduce binding meta caching so that binding will relly less on the reflection during the bind time
