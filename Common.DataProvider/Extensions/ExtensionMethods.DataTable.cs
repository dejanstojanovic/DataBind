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
        public static IEnumerable<T> ToModels<T>(this DataTable table) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(table);
        }

        public static T ToModel<T>(this DataTable table) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(table);
        }
    }
}
