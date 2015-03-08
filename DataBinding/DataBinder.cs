using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroMapper
{
    public static class DataBinder
    {
        public static bool IsModelShallow<T>(T model) where T : class, new()
        {
            foreach (var property in model.GetType().GetProperties())
            {
                if (property.GetValue(model) != null)
                {
                    return false;
                }
            }
            return true;
        }

        public static T BindModel<T>(DataTable dataTable) where T : class, new()
        {
            var models = BindModels<T>(dataTable);
            if (models != null && models.Any())
            {
                return models.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static T BindModel<T>(SqlDataReader dataReader) where T : class, new()
        {
            var models = BindModels<T>(dataReader);
            if (models != null && models.Any())
            {
                return models.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static IEnumerable<T> BindModels<T>(SqlDataReader dataReader) where T : class, new()
        {
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    T item = new T();
                    for (int columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
                    {
                        var objectProperty = GetTargetProperty<T>(dataReader.GetName(columnIndex));
                        if (objectProperty != null)
                        {
                            var dataValue = dataReader.GetValue(columnIndex);
                            if (objectProperty.PropertyType == typeof(List<int>))
                            {
                                objectProperty.SetValue(item, DBNull.Value.Equals(dataValue) ? null : dataValue.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i.Trim())).ToList<int>());
                            }
                            else
                            {
                                objectProperty.SetValue(item, DBNull.Value.Equals(dataValue) ? null : dataValue);
                            }
                        }
                    }

                    yield return item;
                }
            }
            if (!dataReader.IsClosed)
            {
                dataReader.Close();
            }
        }

        public static IEnumerable<T> BindModels<T>(DataTable dataTable) where T : class, new()
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    T item = new T();

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        var objectProperty = GetTargetProperty<T>(column.ColumnName);
                        if (objectProperty != null)
                        {
                            var dataValue = row[column.ColumnName];
                            objectProperty.SetValue(item, DBNull.Value.Equals(dataValue) ? null : dataValue);
                        }
                    }
                    yield return item;
                }
            }
        }

        private static PropertyInfo GetTargetProperty<T>(string name)
        {
            return typeof(T).GetProperties()
                            .Where(p => p.GetCustomAttributes(typeof(DataBind), true)
                                .Where(a => ((DataBind)a).ColumnName == name)
                                .Any()
                                ).FirstOrDefault();
        }

        public static O BindModel<I, O>(I input)
            where I : class, new()
            where O : class, new()
        {
            var output = new O();
            var inputType = input.GetType();
            foreach (var propInfo in inputType.GetProperties())
            {
                var outputProp = GetTargetProperty<O>(propInfo.Name);
                if (outputProp != null)
                {
                    outputProp.SetValue(output, propInfo.GetValue(input));
                }
            }

            return output;
        }
    }
}
