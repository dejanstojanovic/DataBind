using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.DataProvider.Attributes;
using Common.DataProvider.DataBindingMeta;

namespace Common.DataProvider.DataBinding
{
    /// <summary>
    /// The binder class
    /// </summary>
    public static partial class DataBinder
    {
        private static IDictionary<Type, TypeMeta> bindMetaRegistry = new Dictionary<Type, TypeMeta>();

        private static Object locker = new Object();

        private static PropertyInfo getPropertyInfo(Type modelType, string name)
        {
            lock (locker)
            {
                if (!bindMetaRegistry.ContainsKey(modelType))
                {
                    bindMetaRegistry.Add(modelType,
                        new TypeMeta() {
                            ModelType = modelType,
                            PropertyMetaCollection = modelType.GetProperties().Select(p => new PropertyMeta() { Property = p, PropertyName = p.Name, BindAttribute = p.GetCustomAttribute<PropertyBind>() }),
                            BindAttribute = modelType.GetCustomAttribute<ModelBind>()
                        }
                        );
                }
                var modelTypeMeta = bindMetaRegistry[modelType];

                if (modelTypeMeta.BindAttribute != null)
                {
                    return modelTypeMeta.PropertyMetaCollection.Where(p => 
                    (p.BindAttribute != null && p.BindAttribute.ColumnName.Equals(name, modelTypeMeta.BindAttribute.StringComparison)) ||
                    (modelTypeMeta.BindAttribute.AutoMap && p.PropertyName.Equals(name, modelTypeMeta.BindAttribute.StringComparison))
                    ).Select(p => p.Property).FirstOrDefault();
                }
                else {
                    return modelTypeMeta.PropertyMetaCollection.Where(p => p.BindAttribute != null && p.BindAttribute.ColumnName == name).Select(p => p.Property).FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// Get the proerty which is marked with DataBind attribute
        /// </summary>
        /// <typeparam name="T">Type of the object marked with DataBind attribute </typeparam>
        /// <param name="name">ColumnName value of DataBind attribute attached to type property</param>
        /// <returns>PropertyInfo instance if the property is found, null if not found</returns>
        private static PropertyInfo GetTargetProperty<T>(string name)
        {
            return getPropertyInfo(typeof(T), name);
        }


        [Obsolete("Clean approach, but too slow, do not use it!")]
        private static PropertyInfo _GetTargetProperty<T>(string name)
        {
            var modelType = typeof(T);
            var modelBindAttribute = modelType.GetCustomAttributes<ModelBind>().FirstOrDefault();

            if (modelBindAttribute != null)
            {
                return modelType.GetProperties()
                                .Where(p => p.GetCustomAttributes<PropertyBind>().Any() &&
                                            p.GetCustomAttributes<PropertyBind>().First().ColumnName.Equals(name, modelBindAttribute.StringComparison) ||
                                            (modelBindAttribute.AutoMap && p.Name.Equals(name, modelBindAttribute.StringComparison))
                                      ).FirstOrDefault();
            }
            else
            {
                return modelType.GetProperties()
                                .Where(p=> p.GetCustomAttributes<PropertyBind>().Any() &&
                                                           p.GetCustomAttributes<PropertyBind>().First().ColumnName==name
                                       ).FirstOrDefault();
            }
        }
    }
}
