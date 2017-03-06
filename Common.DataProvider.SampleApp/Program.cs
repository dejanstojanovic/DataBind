using DataBinding;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Diagnostics;
using Common.DataProvider.SampleApp.Models;
using Common.DataProvider.Extensions;

namespace Common.DataProvider.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch timer = new Stopwatch();

            ////SqlDataReader reading to models
            timer.Start();
            using (var dal = new DatabaseAccess(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteModels<Order>(
                "Orders_GetAll",
                new Dictionary<String, IConvertible> {
                    { "@Country",null }
                }).ToList();

            }
            timer.Stop();
            Console.WriteLine("Miliseonds: {0}", timer.ElapsedMilliseconds);

            //DataTable to model
            timer.Restart();
            using (var dal = new DatabaseAccess(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteDataTable(
                "Orders_GetAll",
                new Dictionary<String, IConvertible> {
                    { "@Country",null }
                }).ToModels<Order>().ToList();

            }
            timer.Stop();
            Console.WriteLine("Miliseonds: {0}", timer.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
