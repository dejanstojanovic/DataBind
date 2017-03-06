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
        public static IEnumerable<T> ToModels<T>(this DataSet dataset) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(dataset);
        }

        public static IEnumerable<T> ToModels<T>(this DataSet dataset, int tableIndex) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(dataset,tableIndex);
        }

        public static IEnumerable<T> ToModels<T>(this DataSet dataset, String tableName) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(dataset,tableName);
        }

        public static T ToModel<T>(this DataSet dataset) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(dataset);
        }

        public static T ToModel<T>(this DataSet dataset, int tableIndex) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(dataset,tableIndex);
        }

        public static T ToModel<T>(this DataSet dataset, String tableName) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(dataset,tableName);
        }
    }
}
