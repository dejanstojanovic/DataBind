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
        private static Dictionary<String, (PropertyInfo Info, Func<T, Object> Get, Action<T,Object> Set)> properties;

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

                    //Get: Expression.Lambda<Func<T, Object>>(
                    // Expression.Convert(
                    //     Expression.Property(instanceParam, propertyInfo.Name),
                    //     typeof(Object)
                    // ),
                    // instanceParam).Compile(),

                    Get: Expression.Lambda<Func<T, Object>>(
                     Expression.Convert(
                         Expression.Call(instanceParam, propertyInfo.GetGetMethod()),
                         typeof(Object)
                     ),

                     instanceParam).Compile(),

                    Set: Expression.Lambda<Action<T, Object>>(
                        Expression.Call(instanceParam, propertyInfo.GetSetMethod(), Expression.Convert(argumentParam, propertyInfo.PropertyType)),
                        instanceParam,argumentParam
                    ).Compile()

                    ));

                //TODO: Get attribute

                //var typeParam = Expression.Parameter(typeof(Type));
                //var attribute = Expression.Lambda<Func<T, Type, Object>>(
                //    Expression.Convert(
                //        Expression.Call(instanceParam,)
                //            , typeof(Object)
                //        )
                //    ).Compile();

            }
        }

        public static Object Get(T instance, String propertyName)
        {
            if(properties.TryGetValue(propertyName, out var property))
            {
                return property.Get(instance);
            }
            else
            {
                throw new PropertyNotFoundException(instance, propertyName);
            }
        }

        public static void Set(T instance, String propertyName, Object value)
        {
            if (properties.TryGetValue(propertyName, out var property))
            {
                property.Set(instance, value);
            }
            else
            {
                throw new PropertyNotFoundException(instance, propertyName);
            }
        }

    }
}
