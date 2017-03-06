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
            return typeof(T).GetProperties()
                            .Where(p => p.GetCustomAttributes(typeof(DataBind), true)
                                .Where(a => ((DataBind)a).ColumnName == name)
                                .Any()
                                ).FirstOrDefault();
        }
    }
}
