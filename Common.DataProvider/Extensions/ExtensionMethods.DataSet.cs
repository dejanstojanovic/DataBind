using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataProvider.Extensions
{
    public static partial class ExtensionMethods
    {
        #region Bind to model

        #region Bind to multiple
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToModels<T>(this DataSet dataset) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(dataset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataset"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToModels<T>(this DataSet dataset, int tableIndex) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(dataset, tableIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataset"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToModels<T>(this DataSet dataset, String tableName) where T : class, new()
        {
            return DataBinding.DataBinder.BindModels<T>(dataset, tableName);
        }
        #endregion

        #region Bind to single
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public static T ToModel<T>(this DataSet dataset) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(dataset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataset"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public static T ToModel<T>(this DataSet dataset, int tableIndex) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(dataset, tableIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataset"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static T ToModel<T>(this DataSet dataset, String tableName) where T : class, new()
        {
            return DataBinding.DataBinder.BindModel<T>(dataset, tableName);
        }
        #endregion

        #endregion

        #region Bind to dynamic

        #region Bind multiple
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToDynamics(this DataSet dataset)
        {
            return DataBinding.DataBinder.BindDynamics(dataset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToDynamics(this DataSet dataset, int tableIndex)
        {
            return DataBinding.DataBinder.BindDynamics(dataset, tableIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToDynamics(this DataSet dataset, String tableName)
        {
            return DataBinding.DataBinder.BindDynamics(dataset, tableName);
        }

        #endregion

        #region Bind single

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataSet dataset)
        {
            return DataBinding.DataBinder.BindDynamic(dataset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataSet dataset, int tableIndex)
        {
            return DataBinding.DataBinder.BindDynamic(dataset, tableIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataSet dataset, String tableName)
        {
            return DataBinding.DataBinder.BindDynamic(dataset, tableName);
        }
        #endregion

        #endregion







    }
}
