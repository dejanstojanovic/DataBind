using Common.DataProvider.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataProvider.DataBindingMeta
{
    public class PropertyMeta
    {
        public PropertyInfo Property { get; set; }
        public String PropertyName { get; set; }
        public PropertyBind BindAttribute { get; set; }
    }
}
