﻿using Databind.Binding.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Databind.Binding
{
    public static class Instance<T> where T : new()
    {
        public static readonly Func<T> New;
        private static Dictionary<String, (PropertyInfo Info, Func<T, Object> Get, Action<T, Object> Set)> properties;

        public static IEnumerable<String> GetPropertyNames() => properties.Keys;

        static Instance()
        {
            New = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
            properties = new Dictionary<string, (PropertyInfo Info, Func<T, object> Get, Action<T, object> Set)>();
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var instanceParam = Expression.Parameter(typeof(T));
                var argumentParam = Expression.Parameter(typeof(Object));

                properties.Add(
                   propertyInfo.Name, (
                   Info: propertyInfo,
                   Get: Expression.Lambda<Func<T, Object>>(
                          Expression.Convert(
                              Expression.Call(instanceParam, propertyInfo.GetGetMethod()),
                              typeof(Object)
                          ),
                        instanceParam).Compile(),

                   Set: Expression.Lambda<Action<T, Object>>(
                          Expression.Call(instanceParam, propertyInfo.GetSetMethod(), Expression.Convert(argumentParam, propertyInfo.PropertyType)),
                          instanceParam, argumentParam
                        ).Compile()

                    ));
            }
        }

        public static Object Get(T instance, String propertyName)
        {
            return properties[propertyName].Get(instance);
        }

        public static void Set(T instance, String propertyName, Object value)
        {
            properties[propertyName].Set(instance, value);
        }

    }
}