using System.Text.Json.Serialization;

namespace PropToString.Test;

[PropsToString]
public partial class MyClasss
{
   //[PropsToStringIgnore]
   [PropToString("title")]
   public string Title { get; set; } = string.Empty;
   [PropToString("artist")]
   public string Author { get; set; } = string.Empty;
   public int Year { get; set; }
   public string Narrator { get; set; } = string.Empty;
}