using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DataBind:Attribute
    {
        #region Fields
        private string columnName;
        #endregion
 
        #region Properties
        public string ColumnName
        {
            get
            {
                return this.columnName;
            }
            set
            {
                this.columnName = value;
            }
        }
        #endregion
 
        #region Constructors
        public DataBind(string columnName)
        {
            this.columnName = columnName;
        }
        #endregion
    }
}
