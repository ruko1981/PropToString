// Auto Generated Source Code
// Author : Michael Diehl

using global::System.Text.Json;

namespace PropToString.Test
{
   public partial class MyClass
   {
      public string ToPropertyString() => $"-metadata Author={JsonSerializer.Serialize(Author)} -metadata Year={JsonSerializer.Serialize(Year)} -metadata Narrator={JsonSerializer.Serialize(Narrator)}";
   }
}