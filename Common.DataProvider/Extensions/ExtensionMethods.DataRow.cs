using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataProvider.Extensions
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Bind DataRow to object instance of type T
        /// </summary>
        /// <typeparam name="T">Output type to bind to</typeparam>
        /// <param name="row">Input DataRow object</param>
        /// <returns>Object instance of type T</returns>
        public static T ToModels<T>(this DataRow row) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(row);
        }

        /// <summary>
        /// Bind DataRow to dynamic variable
        /// </summary>
        /// <param name="row">Input DataRow object</param>
        /// <returns>dynamic type object instance</returns>
        public static dynamic ToDynamic(this DataRow row) 
        {
            return DataBinding.DataBinder.BindDynamic(row);
        }
    }
}
