using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataProvider.Attributes
{
    /// <summary>
    /// Attribute for marking the properties and columns to which to bind the data to
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PropertyBind : Attribute
    {
        #region Fields
        private string columnName;
        #endregion

        #region Properties
        /// <summary>
        /// Name of the column to bind the property to
        /// </summary>
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
        public PropertyBind(string columnName)
        {
            this.columnName = columnName;
        }
        #endregion
    }
}
