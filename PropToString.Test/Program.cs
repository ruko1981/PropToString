using PropToString;
namespace PropToString.Test;

internal class Program
{
   static void Main(string[] args)
   {
      MyClass myClass = new()
      {
         Author = "Mike",
         Title = "The Book",
         Year = 2022,
         Narrator = "Keith Richards"
      };

      

      Console.WriteLine(myClass.ToPropertyString());


   }
}