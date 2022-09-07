// Auto Generated Source Code
// Author : Michael Diehl

using global::System.Text.Json;

namespace PropToString.Test
{
   public partial class MyOtherClass
   {
      public string ToPropertyString() => $"-metadata MyProperty={JsonSerializer.Serialize(MyProperty)}";
   }
}