# DataBind
Simple data binding class for easy binding of
 - DataRow to single POCO class
 - DataTable to single POCO class
 
  ```csharp
 using (var dal = new SqlProvider(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteDataTable(
                "Orders_GetAll",
                new Dictionary<String, IConvertible> {
                    { "@Country",null }
                }).ToModel<Order>();

            }
 ```
 - DataTable to multiple POCO classes
 
 ```csharp
 using (var dal = new SqlProvider(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteDataTable(
                "Orders_GetAll",
                new Dictionary<String, IConvertible> {
                    { "@Country",null }
                }).ToModels<Order>().ToList();

            }
 ```
 - DataSet to single POCO class
 - DataSet to multiple POCO classes
 - SqlDataReader to single POCO class
 
 ```csharp
 using (var dal = new SqlProvider(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteModel<Order>(
                "Orders_GetAll",
                new Dictionary<String, IConvertible> {
                    { "@Country",null }
                });
            }
```
 - SqlDataReader to multiple POCO classes
 
 ```csharp
 using (var dal = new SqlProvider(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteModels<Order>(
                "Orders_GetAll",
                new Dictionary<String, IConvertible> {
                    { "@Country",null }
                }).ToList();
            }
```

 - One POCO class to another POCO class
 - ADO objects to dynamic object instance

### Sample

> **Note:**
For this sample I used Northwind database provided by Microsoft. You can download it from https://northwinddatabase.codeplex.com/

```csharp
 using (var dal = new SqlProvider(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
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
```

> **Note:**
Attribute **ModelBind** on the class level is used to perform mapping even for the proeprties which are not decorated. In this case case it will try to match property name to source name with StringComparison option.

Sample stored procedure for NORTWND database
```sql
CREATE PROCEDURE Orders_GetAll 
@Country VARCHAR(100) = NULL

AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM dbo.Orders WHERE ShipCountry = ISNULL(@Country,ShipCountry)
END
GO
```

> **Note:**
Binding is done using reflection so it can hurt performance. I am planning to introduce binding meta caching so that binding will relly less on the reflection during the bind time
