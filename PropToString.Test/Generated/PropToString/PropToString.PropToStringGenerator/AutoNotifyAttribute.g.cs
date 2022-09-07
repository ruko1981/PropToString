
using System;
namespace PropToString
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    [System.Diagnostics.Conditional("AutoNotifyGenerator_DEBUG")]
    sealed class PropToStringAttribute : Attribute
    {
        public PropToStringAttribute()
        {
        }
        public string PropertyName { get; set; }
    }
}
