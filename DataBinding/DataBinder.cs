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

        #region Single instance binding

        /// <summary>
        /// Bind DataRow to class instance of type T
        /// </summary>
        /// <typeparam name="T">Target instance type to bind to</typeparam>
        /// <param name="dataRow">Source DataRow instance to bind from</param>
        /// <returns>Class instance of type T</returns>
        public static T BindModel<T>(DataRow dataRow) where T : class, new()
        {
            T item = new T();

            if (dataRow.Table != null)
            {
                foreach (DataColumn column in dataRow.Table.Columns)
                {
                    var objectProperty = GetTargetProperty<T>(column.ColumnName);
                    if (objectProperty != null)
                    {
                        var dataValue = dataRow[column.ColumnName];
                        objectProperty.SetValue(item, DBNull.Value.Equals(dataValue) ? null : dataValue);
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// Bind DataTable to class instance of type T
        /// </summary>
        /// <typeparam name="T">Target instance type to bind to</typeparam>
        /// <param name="dataTable">Source DataTable instance to bind from</param>
        /// <returns>Class instance of type T</returns>
        public static T BindModel<T>(DataTable dataTable) where T : class, new()
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return BindModel<T>(dataTable.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// Bind SqlDataReader to class instance of type T
        /// </summary>
        /// <typeparam name="T">Target instance type to bind to</typeparam>
        /// <param name="dataReader">Source SqlDataReader instance to bind from</param>
        /// <returns>Class instance of type T</returns>
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

        /// <summary>
        /// Bind instance of type I to class instance of type O
        /// </summary>
        /// <typeparam name="T">Target instance type to bind to</typeparam>
        /// <typeparam name="O">Source instance type to bind from</typeparam>
        /// <param name="input">Class instance of type I to bind from</param>
        /// <returns>Class instance of type O</returns>
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

        #endregion

        #region Multiple instance binding

        /// <summary>
        /// Returns multiple class instances of type T from SqlDataReader
        /// </summary>
        /// <typeparam name="T">Target IEnumerable of type T to bind to</typeparam>
        /// <param name="dataReader">Source SqlDataReader instance to bind from</param>
        /// <returns>IEnumerable of type T</returns>
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

        /// <summary>
        /// Returns multiple class instances of type T from DataTable
        /// </summary>
        /// <typeparam name="T">Target IEnumerable of type T to bind to</typeparam>
        /// <param name="dataTable">Source DataTable instance to bind from</param>
        /// <returns>IEnumerable of type T</returns>
        public static IEnumerable<T> BindModels<T>(DataTable dataTable) where T : class, new()
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    yield return BindModel<T>(row);
                }
            }
        }

        /// <summary>
        /// Returns multiple class instances of type O from IEnumeerable of type I
        /// </summary>
        /// <typeparam name="I">Target IEnumerable of type I to bind from</typeparam>
        /// <typeparam name="O">Target IEnumerable of type O to bind to</typeparam>
        /// <param name="inputs">IEnumerable of type I to bind from</param>
        /// <returns>IEnumerable of type O</returns>
        public static IEnumerable<O> BindModels<I, O>(IEnumerable<I> inputs)
            where I : class, new()
            where O : class, new()
        {
            foreach (I input in inputs)
            {
                yield return BindModel<I, O>(input);
            }
        }


        #endregion

        private static PropertyInfo GetTargetProperty<T>(string name)
        {
            return typeof(T).GetProperties()
                            .Where(p => p.GetCustomAttributes(typeof(DataBind), true)
                                .Where(a => ((DataBind)a).ColumnName == name)
                                .Any()
                                ).FirstOrDefault();
        }

        
    }
}
