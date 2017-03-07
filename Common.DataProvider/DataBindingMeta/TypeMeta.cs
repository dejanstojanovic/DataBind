﻿using Common.DataProvider.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataProvider.DataBindingMeta
{
    public class TypeMeta
    {
        public Type ModelType { get; set; }
        public ModelBind BindAttribute { get; set; }
        public IEnumerable<PropertyMeta> PropertyMetaCollection {get;set;}
    }
}
