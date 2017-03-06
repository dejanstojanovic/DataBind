using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataProvider.DataBinding
{
    public static partial class DataBinder
    {
        #region Single instance binding

        /// <summary>
        /// Bind single DataRow to dynamic object
        /// </summary>
        /// <param name="dataRow">DataRow to be transformad to an object</param>
        /// <returns>Dynamic object instance</returns>
        public static dynamic BindDynamic(DataRow dataRow)
        {
            dynamic result = null;
            if (dataRow != null)
            {
                result = new ExpandoObject();
                var resultDictionary = (IDictionary<string, object>)result;
                foreach (DataColumn column in dataRow.Table.Columns)
                {
                    var dataValue = dataRow[column.ColumnName];
                    resultDictionary.Add(column.ColumnName, DBNull.Value.Equals(dataValue) ? null : dataValue);
                }
            }
            return result;
        }


        /// <summary>
        /// Bind first SqlDataReader record to dynamic object
        /// </summary>
        /// <param name="dataReader">SqlDataReader to acquire data from</param>
        /// <returns>Dynamic object instance</returns>
        public static dynamic BindDynamic(SqlDataReader dataReader)
        {
            var models = BindDynamics(dataReader, true);
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
        /// Bind first DataRow from DataTable to dynamic object
        /// </summary>
        /// <param name="dataTable">DataTable to acquire data from</param>
        /// <returns>Dynamic object instance</returns>
        public static dynamic BindDynamic(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return BindDynamic(dataTable.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// Bind first DataRow from DataTable with specific index from the DataSet to dynamic object
        /// </summary>
        /// <param name="dataSet">DataSet to bind data from</param>
        /// <param name="tableIndex">Index of the DataTable in DataSet to bind the data from</param>
        /// <returns>Dynamic object instance</returns>
        public static dynamic BindDynamic(DataSet dataSet, int tableIndex = 0)
        {
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables.Count - 1 >= tableIndex)
            {
                return BindDynamic(dataSet.Tables[tableIndex]);
            }
            return null;
        }

        /// <summary>
        /// Bind first DataRow from DataTable with specific table name from the DataSet to dynamic object
        /// </summary>
        /// <param name="dataSet">DataSet to bind data from</param>
        /// <param name="tableName">Name of the DataTable in DataSet to bind the data from</param>
        /// <returns>Dynamic object instance</returns>
        public static dynamic BindDynamic(DataSet dataSet, string tableName)
        {
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                return BindDynamic(dataSet.Tables[tableName]);
            }
            return null;
        }

        #endregion

        #region Multiple instance binding

        /// <summary>
        /// Returns multiple dynamics object instances created from a DataReader
        /// </summary>
        /// <param name="dataReader">Reader used to itereate the data and bind to dynamic object instance</param>
        /// <returns>IEnumerable of dynamic object instances</returns>
        public static IEnumerable<dynamic> BindDynamics(SqlDataReader dataReader, bool takeFirstOnly = false)
        {
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    dynamic item = new ExpandoObject();
                    for (int columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
                    {
                        var resultDictionary = (IDictionary<string, object>)item;
                        var dataValue = dataReader.GetValue(columnIndex);
                        resultDictionary.Add(dataReader.GetName(columnIndex), DBNull.Value.Equals(dataValue) ? null : dataValue);
                    }
                    yield return item;

                    if (takeFirstOnly)
                    {
                        break;
                    }
                }
            }

            if (!dataReader.IsClosed)
            {
                dataReader.Close();
            }
        }

        /// <summary>
        /// Returns multiple dynamics object instances created from a DataTable
        /// </summary>
        /// <param name="dataTable">DataTable containing data to be transformed to dynamic objects</param>
        /// <returns>IEnumerable of dynamic object instances</returns>
        public static IEnumerable<dynamic> BindDynamics(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return BindDynamic(row);
            }
        }

        /// <summary>
        /// Returns multiple dynamics object instances created from a DataSet table with specific index
        /// </summary>
        /// <param name="dataSet">DataTable containing DataTable to be transformed to dynamic objects</param>
        /// <param name="tableIndex">Index of DataTable in DataSet to be used for binding (default is 0)</param>
        /// <returns>IEnumerable of dynamic object instances</returns>
        public static IEnumerable<dynamic> BindDynamics(DataSet dataSet, int tableIndex = 0)
        {
            return BindDynamics(dataSet.Tables[tableIndex]);
        }

        /// <summary>
        /// Returns multiple dynamics object instances created from a DataSet table with specific name
        /// </summary>
        /// <param name="dataSet">DataTable containing DataTable to be transformed to dynamic objects</param>
        /// <param name="tableName">Name of DataTable in DataSet to be used for binding</param>
        /// <returns>IEnumerable of dynamic object instances</returns>
        public static IEnumerable<dynamic> BindDynamics(DataSet dataSet, string tableName)
        {
            return BindDynamics(dataSet.Tables[tableName]);
        }
        #endregion
    }
}
