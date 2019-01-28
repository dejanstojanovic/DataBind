using System.Collections.Generic;
using System.Data;

namespace Databind.Extensions
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToModels<T>(this DataTable table) where T : class, new()
        {
            return Binding.DataBinder.BindModels<T>(table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T ToModel<T>(this DataTable table) where T : class, new()
        {
            return Binding.DataBinder.BindModel<T>(table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToDynamics(this DataTable table)
        {
            return Binding.DataBinder.BindDynamics(table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataTable table)
        {
            return Binding.DataBinder.BindDynamic(table);
        }
    }
}
