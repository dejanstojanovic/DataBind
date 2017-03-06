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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToModels<T>(this DataTable table) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T ToModel<T>(this DataTable table) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToDynamics(this DataTable table)
        {
            return DataBinding.DataBinder.BindDynamics(table);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataTable table)
        {
            return DataBinding.DataBinder.BindDynamic(table);
        }
    }
}
