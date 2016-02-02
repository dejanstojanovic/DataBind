using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding
{
    public class DatabaseAccess : IDisposable
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
            set
            {
                connection = value;
            }
        }
        #endregion

        #region Get Sql object instances

        private SqlDataAdapter GetAdapter(string procedureName, Dictionary<string, IConvertible> parameters)
        {
            return new SqlDataAdapter(this.GetCommand(procedureName, parameters));
        }

        private SqlDataAdapter GetAdapter(SqlCommand command)
        {
            return new SqlDataAdapter(command);
        }

        private SqlCommand GetCommand(string procedureName, Dictionary<string, IConvertible> parameters = null)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand(procedureName, this.connection);


            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    command.Parameters.Add(new SqlParameter(param.Key, param.Value != null ? param.Value : DBNull.Value));
                }
            }
            return command;
        }
        #endregion

        #region Data operations

        public int ExecuteNoOutput(string procedureName, Dictionary<string, IConvertible> parameters = null)
        {
            return this.GetCommand(procedureName, parameters).ExecuteNonQuery();
        }

        public T ExecuteSimpleOutput<T>(string procedureName, Dictionary<string, IConvertible> parameters = null) where T : IConvertible
        {
            var returnValue = this.GetCommand(procedureName, parameters).ExecuteScalar();
            return (T)returnValue;
        }

        public SqlDataReader ExecuteReader(string procedureName, Dictionary<string, IConvertible> parameters = null)
        {
            return this.GetCommand(procedureName, parameters).ExecuteReader();
        }

        public DataTable ExecuteDataTable(string procedureName, Dictionary<string, IConvertible> parameters = null)
        {
            DataTable dataTable = new DataTable();
            this.GetAdapter(procedureName, parameters).Fill(dataTable);
            return dataTable;
        }

        public DataSet ExecuteDataSet(string procedureName, Dictionary<string, IConvertible> parameters = null)
        {
            DataSet dataSet = new DataSet();
            this.GetAdapter(procedureName, parameters).Fill(dataSet);
            return dataSet;
        }

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
    }
}
