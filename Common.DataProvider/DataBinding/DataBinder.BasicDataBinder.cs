using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.DataProvider.Attributes;

namespace Common.DataProvider.DataBinding
{
    /// <summary>
    /// The binder class
    /// </summary>
    public static partial class DataBinder
    {
        /// <summary>
        /// Get the proerty which is marked with DataBind attribute
        /// </summary>
        /// <typeparam name="T">Type of the object marked with DataBind attribute </typeparam>
        /// <param name="name">ColumnName value of DataBind attribute attached to type property</param>
        /// <returns>PropertyInfo instance if the property is found, null if not found</returns>
        private static PropertyInfo GetTargetProperty<T>(string name)
        {
            var modelType = typeof(T);
            var modelBindAttribute = modelType.GetCustomAttributes<ModelBind>().FirstOrDefault();

            if (modelBindAttribute != null)
            {
                return modelType.GetProperties()
                                .Where(p => p.GetCustomAttributes<PropertyBind>().Any() && 
                                            p.GetCustomAttributes<PropertyBind>().First().ColumnName.Equals(name, modelBindAttribute.StringComparison) ||
                                            (modelBindAttribute.AutoMap && p.Name.Equals(name, modelBindAttribute.StringComparison))
                                      ).FirstOrDefault();
            }
            else
            {
                return modelType.GetProperties()
                                .Where(p=> p.GetCustomAttributes<PropertyBind>().Any() &&
                                                           p.GetCustomAttributes<PropertyBind>().First().ColumnName==name
                                       ).FirstOrDefault();
            }
        }
    }
}
