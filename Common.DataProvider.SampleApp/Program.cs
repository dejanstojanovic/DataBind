using DataBinding;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Diagnostics;
using Common.DataProvider.SampleApp.Models;

namespace Common.DataProvider.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
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

            Console.WriteLine("Miliseonds: {0}",timer.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
