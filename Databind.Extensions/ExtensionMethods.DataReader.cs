using System.Collections.Generic;
using System.Data.SqlClient;

namespace Databind.Extensions
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
            return Binding.DataBinder.BindModels<T>(reader);
        }

		/// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T ToModel<T>(this SqlDataReader reader) where T : class, new()
        {
            return Binding.DataBinder.BindModel<T>(reader);
        }


        public static IEnumerable<dynamic> ToDynamics(this SqlDataReader reader) 
        {
            return Binding.DataBinder.BindDynamics(reader);
        }

        public static dynamic ToDynamic(this SqlDataReader reader)
        {
            return Binding.DataBinder.BindDynamic(reader);
        }
    }
}
