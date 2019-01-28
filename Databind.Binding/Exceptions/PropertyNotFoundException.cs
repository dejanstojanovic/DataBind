using System;

namespace Databind.Binding.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public Type ObjectType { get; private set; }
        public String PropertyName { get; private set; }

        public override string Message
        {
            get
            {
                return $"Object of type {this.ObjectType.FullName} does not contain property {this.PropertyName}";
            }
        }

        public PropertyNotFoundException(Object instance, String propertyName) : this(instance.GetType(), propertyName)
        {
        }

        public PropertyNotFoundException(Type instanceType, String propertyName)
        {
            this.ObjectType = instanceType;
            this.PropertyName = PropertyName;
        }
    }
}
