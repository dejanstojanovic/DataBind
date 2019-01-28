using System;
using System.Collections.Generic;
using System.Data;

namespace Databind.Extensions
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
            return Binding.DataBinder.BindModels<T>(dataset);
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
            return Binding.DataBinder.BindModels<T>(dataset, tableIndex);
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
            return Binding.DataBinder.BindModels<T>(dataset, tableName);
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
            return Binding.DataBinder.BindModel<T>(dataset);
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
            return Binding.DataBinder.BindModel<T>(dataset, tableIndex);
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
            return Binding.DataBinder.BindModel<T>(dataset, tableName);
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
            return Binding.DataBinder.BindDynamics(dataset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToDynamics(this DataSet dataset, int tableIndex)
        {
            return Binding.DataBinder.BindDynamics(dataset, tableIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> ToDynamics(this DataSet dataset, String tableName)
        {
            return Binding.DataBinder.BindDynamics(dataset, tableName);
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
            return Binding.DataBinder.BindDynamic(dataset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataSet dataset, int tableIndex)
        {
            return Binding.DataBinder.BindDynamic(dataset, tableIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static dynamic ToDynamic(this DataSet dataset, String tableName)
        {
            return Binding.DataBinder.BindDynamic(dataset, tableName);
        }
        #endregion

        #endregion







    }
}
