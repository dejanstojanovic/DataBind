using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Diagnostics;
using Databind.SampleApp.Models;
using Databind.Providers;
using Databind.Extensions;

namespace Databind.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch timer = new Stopwatch();

            using (var dal = new SqlProvider(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteDataTable(
                "Orders_GetAll");//.ToModels<Order>().ToList();

            }

            ////SqlDataReader reading to models
            timer.Start();
            using (var dal = new SqlProvider(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteModels<Order>(
                "Orders_GetAll").ToList();

            }
            timer.Stop();
            Console.WriteLine($"ExecuteModels: {timer.ElapsedMilliseconds}");


            //DataTable to model
            timer.Restart();
            using (var dal = new SqlProvider(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteDataTable(
                "Orders_GetAll").ToModels<Order>().ToList();

            }
            timer.Stop();
            Console.WriteLine($"ExecuteDataTable.ToModels: {timer.ElapsedMilliseconds}" );

            //DataTable only
            timer.Restart();
            using (var dal = new SqlProvider(ConfigurationManager.ConnectionStrings["db.connection"].ToString()))
            {
                var result = dal.ExecuteDataTable(
                "Orders_GetAll"
                );//.ToModels<Order>().ToList();

            }
            timer.Stop();
            Console.WriteLine($"ExecuteDataTable: {timer.ElapsedMilliseconds}");


            Console.ReadLine();
        }
    }
}
