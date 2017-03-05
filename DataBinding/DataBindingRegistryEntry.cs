using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding
{
    public class DataBindingRegistryEntry
    {
        public PropertyInfo TargetProperty { get; set; }
        public String SourceName { get; set; }
    }
}
