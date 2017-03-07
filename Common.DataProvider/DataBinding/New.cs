using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataProvider.DataBinding
{
    /* Lambda sample from http://stackoverflow.com/questions/6582259/fast-creation-of-objects-instead-of-activator-createinstancetype */
    public static class New<T> where T : new()
    {
        public static readonly Func<T> Instance = Expression.Lambda<Func<T>>
                                                  (
                                                   Expression.New(typeof(T))
                                                  ).Compile();
    }
}
