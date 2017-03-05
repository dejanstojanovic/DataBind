using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding
{
    public static class DataBindingRegistry
    {
        private static IDictionary<String, List<DataBindingRegistryEntry>> registry;
        private static Object locker;

        static DataBindingRegistry()
        {
            locker = new Object();
            registry = new Dictionary<String, List<DataBindingRegistryEntry>>();
        }

        public static PropertyInfo AddIfAbsent(Type type,String source)
        {
            lock (locker)
            {
                List<DataBindingRegistryEntry> entries = registry.Where(r=>r.Key==type.FullName).Select(r=>r.Value).FirstOrDefault();
                if (entries == null)
                {
                    entries = new List<DataBindingRegistryEntry>();
                    registry.Add(type.FullName, entries);
                }

                var entry = entries.Where(e => e.SourceName == source).FirstOrDefault();
                if (entry==null)
                {
                    var propInfo = type.GetProperties()
                            .Where(p => p.GetCustomAttributes(typeof(DataBind), true)
                                .Where(a => ((DataBind)a).ColumnName == source)
                                .Any()
                                ).FirstOrDefault();
                    if (propInfo != null)
                    {
                        entry = new DataBindingRegistryEntry() { TargetProperty = propInfo, SourceName = source };
                        entries.Add(entry);
                        return entry.TargetProperty;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return entry.TargetProperty;
                }



            }
        }



    }
}
