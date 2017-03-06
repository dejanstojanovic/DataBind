using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataProvider.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ModelBind : Attribute
    {
        private bool autoMap;
        private StringComparison stringComparison;

        public bool AutoMap { get
            {
                return this.autoMap;
            }
        }

        public StringComparison StringComparison
        {
            get
            {
                return this.stringComparison;
            }
        }

        public ModelBind()
        {
            this.autoMap = true;
            this.stringComparison = StringComparison.InvariantCultureIgnoreCase;
        }
        public ModelBind(bool autoMap)
        {
            this.autoMap = autoMap;
            this.stringComparison = StringComparison.InvariantCultureIgnoreCase;
        }

        public ModelBind(bool autoMap, StringComparison stringComparison)
        {
            this.autoMap = autoMap;
            this.stringComparison = stringComparison;
        }
    }
}
