using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToModels<T>(this SqlDataReader reader) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(reader);
        }

		/// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T ToModel<T>(this SqlDataReader reader) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(reader);
        }


        public static IEnumerable<dynamic> ToDynamics(this SqlDataReader reader) 
        {
            return DataBinding.DataBinder.BindDynamics(reader);
        }

        public static dynamic ToDynamic(this SqlDataReader reader)
        {
            return DataBinding.DataBinder.BindDynamic(reader);
        }
    }
}
