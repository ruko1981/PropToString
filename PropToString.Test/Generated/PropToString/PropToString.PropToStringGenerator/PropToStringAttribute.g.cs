// Auto Generated Source Code
// Author : Michael Diehl

using global::System;

namespace PropToString
{
   [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
   [System.Diagnostics.Conditional("PropsToStringGenerator_DEBUG")]
   sealed class PropToString : Attribute
   {
      public PropToString()
      {
      }
      public string Template { get; set; } = string.Empty;
      public bool NewLines {get; set;} = true;
   }
}
