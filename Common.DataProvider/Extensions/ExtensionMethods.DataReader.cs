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
        public static IEnumerable<T> ToModels<T>(this SqlDataReader reader) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(reader);
        }

        public static T ToModel<T>(this SqlDataReader reader) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(reader);
        }
    }
}
