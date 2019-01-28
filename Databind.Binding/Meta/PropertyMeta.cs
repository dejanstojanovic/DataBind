using Databind.Binding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Databind.Binding.Meta
{
    public class PropertyMeta
    {
        public PropertyInfo Property { get; set; }
        public String PropertyName { get; set; }
        public PropertyBind BindAttribute { get; set; }
    }
}
