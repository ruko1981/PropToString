// Auto Generated Source Code
// Author : Michael Diehl

using global::System;

namespace PropToString
{
   [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
   sealed class PropsToString : Attribute
   {
      public PropsToString()
      {
      }
      public string Template {get; set; } = string.Empty;
      public bool NewLines {get; set; } = true;
   }
}
