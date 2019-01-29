using Databind.Binding;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Databind.Providers
{
    public class SqlProvider : IDisposable
    {
        #region Fields
        private SqlConnection connection = null;
        #endregion

        #region Properties
        public SqlConnection Connection
        {
            get
            {
                return connection;
            }
        }
        #endregion

        #region Get Adapter instance

        private SqlDataAdapter GetAdapter(String procedureName, KeyValuePair<string, IConvertible>[] parameters)
        {
            return new SqlDataAdapter(this.GetCommand(procedureName, parameters.ToDictionary(t => t.Key, t => t.Value)));
        }

        private SqlDataAdapter GetAdapter(String procedureName, Dictionary<string, IConvertible> parameters)
        {
            return new SqlDataAdapter(this.GetCommand(procedureName, parameters));
        }

        private SqlDataAdapter GetAdapter(String command)
        {
            return new SqlDataAdapter(new SqlCommand(command));
        }

        private SqlDataAdapter GetAdapter(SqlCommand command)
        {
            return new SqlDataAdapter(command);
        }
        #endregion

        #region Get command instance
        private SqlCommand GetCommand(string procedureName, KeyValuePair<string, IConvertible>[] parameters)
        {
            return GetCommand(procedureName, parameters.ToDictionary(t => t.Key, t => t.Value));
        }

        private SqlCommand GetCommand(string procedureName, Dictionary<string, IConvertible> parameters)
        {
            var command = GetCommand(procedureName);
            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    command.Parameters.Add(new SqlParameter(param.Key, param.Value != null ? param.Value : DBNull.Value));
                }
            }
            return command;
        }

        private SqlCommand GetCommand(string procedureName)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand(procedureName, this.connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        #endregion

        #region Data operations

        #region No output execute stored procedure

        public int ExecuteNoOutput(string procedureName, Dictionary<string, IConvertible> parameters)
        {
            return this.GetCommand(procedureName, parameters).ExecuteNonQuery();
        }

        public int ExecuteNoOutput(string procedureName, params KeyValuePair<string, IConvertible>[] parameters)
        {
            return this.GetCommand(procedureName, parameters).ExecuteNonQuery();
        }

        public int ExecuteNoOutput(string procedureName)
        {
            return this.GetCommand(procedureName).ExecuteNonQuery();
        }

        #endregion

        #region Non reference value output
        public T ExecuteSimpleOutput<T>(string procedureName, Dictionary<string, IConvertible> parameters) where T : IConvertible
        {
            var returnValue = this.GetCommand(procedureName, parameters).ExecuteScalar();
            return (T)returnValue;
        }

        public T ExecuteSimpleOutput<T>(string procedureName, params KeyValuePair<string, IConvertible>[] parameters) where T : IConvertible
        {
            var returnValue = this.GetCommand(procedureName, parameters).ExecuteScalar();
            return (T)returnValue;
        }

        public T ExecuteSimpleOutput<T>(string procedureName) where T : IConvertible
        {
            var returnValue = this.GetCommand(procedureName).ExecuteScalar();
            return (T)returnValue;
        }

        #endregion

        #region DataReader output

        public SqlDataReader ExecuteReader(string procedureName, Dictionary<string, IConvertible> parameters)
        {
            return this.GetCommand(procedureName, parameters).ExecuteReader();
        }

        public SqlDataReader ExecuteReader(string procedureName, params KeyValuePair<string, IConvertible>[] parameters)
        {
            return this.GetCommand(procedureName, parameters).ExecuteReader();
        }

        public SqlDataReader ExecuteReader(string procedureName)
        {
            return this.GetCommand(procedureName).ExecuteReader();
        }

        #endregion

        #region DataTable output
        public DataTable ExecuteDataTable(string procedureName, Dictionary<String, IConvertible> parameters = null)
        {
            DataTable dataTable = new DataTable();
            this.GetAdapter(procedureName, parameters).Fill(dataTable);
            return dataTable;
        }

        public DataTable ExecuteDataTable(string procedureName, params KeyValuePair<String, IConvertible>[] parameters)
        {
            return ExecuteDataTable(procedureName, parameters.ToDictionary(t => t.Key, t => t.Value));
        }
        public DataTable ExecuteDataTable(String procedureName)
        {
            return ExecuteDataTable(procedureName, new Dictionary<String, IConvertible>());
        }

        #endregion

        #region DataSet output
        public DataSet ExecuteDataSet(string procedureName, Dictionary<string, IConvertible> parameters)
        {
            DataSet dataSet = new DataSet();
            this.GetAdapter(procedureName, parameters).Fill(dataSet);
            return dataSet;
        }

        public DataSet ExecuteDataSet(string procedureName, params KeyValuePair<string, IConvertible>[] parameters)
        {
            DataSet dataSet = new DataSet();
            this.GetAdapter(procedureName, parameters).Fill(dataSet);
            return dataSet;
        }

        public DataSet ExecuteDataSet(string procedureName)
        {
            DataSet dataSet = new DataSet();
            this.GetAdapter(procedureName).Fill(dataSet);
            return dataSet;
        }

        #endregion

        #region Strongly typed model object output

        public T ExecuteModel<T>(string procedureName, Dictionary<string, IConvertible> parameters) where T : class, new()
        {
            return DataBinder.BindModel<T>(ExecuteReader(procedureName, parameters));
        }

        public T ExecuteModel<T>(string procedureName, params KeyValuePair<string, IConvertible>[] parameters) where T : class, new()
        {
            return DataBinder.BindModel<T>(ExecuteReader(procedureName, parameters));
        }

        public T ExecuteModel<T>(string procedureName) where T : class, new()
        {
            return DataBinder.BindModel<T>(ExecuteReader(procedureName));
        }

        public IEnumerable<T> ExecuteModels<T>(string procedureName, Dictionary<string, IConvertible> parameters, StringComparison matchOption = StringComparison.InvariantCultureIgnoreCase) where T : class, new()
        {
            return DataBinder.BindModels<T>(ExecuteReader(procedureName, parameters));
        }

        public IEnumerable<T> ExecuteModels<T>(string procedureName, params KeyValuePair<string, IConvertible>[] parameters) where T : class, new()
        {
            return DataBinder.BindModels<T>(ExecuteReader(procedureName, parameters));
        }

        public IEnumerable<T> ExecuteModels<T>(string procedureName) where T : class, new()
        {
            return DataBinder.BindModels<T>(ExecuteReader(procedureName));
        }

        #endregion

        #region Dynamic object output
        public dynamic ExecuteDynamic(string procedureName, Dictionary<string, IConvertible> parameters)
        {
            return DataBinder.BindDynamic(ExecuteReader(procedureName, parameters));
        }

        public dynamic ExecuteDynamic(string procedureName, params KeyValuePair<string, IConvertible>[] parameters)
        {
            return DataBinder.BindDynamic(ExecuteReader(procedureName, parameters));
        }

        public dynamic ExecuteDynamic(string procedureName)
        {
            return DataBinder.BindDynamic(ExecuteReader(procedureName));
        }

        public IEnumerable<dynamic> ExecuteDynamics(string procedureName, Dictionary<string, IConvertible> parameters)
        {
            return DataBinder.BindDynamics(ExecuteReader(procedureName, parameters));
        }

        public IEnumerable<dynamic> ExecuteDynamics(string procedureName, params KeyValuePair<string, IConvertible>[] parameters)
        {
            return DataBinder.BindDynamics(ExecuteReader(procedureName, parameters));
        }

        public IEnumerable<dynamic> ExecuteDynamics(string procedureName)
        {
            return DataBinder.BindDynamics(ExecuteReader(procedureName));
        }

        #endregion

        #endregion

        #region Dispose
        private bool disposed = false;

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                if (this.connection != null)
                {
                    this.connection.Close();
                    this.connection.Dispose();
                }
            }
        }


        #endregion

        #region Constructors

        public SqlProvider(String connectionString) : this(new SqlConnection(connectionString))
        {

        }

        public SqlProvider(SqlConnection connection)
        {
            this.connection = connection;
        }

        #endregion
    }
}
