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
        public static T ToModels<T>(this DataRow row, StringComparison matchOption) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(row);
        }
    }
}
