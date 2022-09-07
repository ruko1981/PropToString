using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Immutable;
using System.Text;

namespace PropToString;
public readonly struct ClassInfo : IEquatable<ClassInfo>
{
   public readonly string? Namespace { get; }
   public readonly string Name { get; }
   public readonly ImmutableArray<IPropertySymbol> PropertyNames { get; }

   public ClassInfo(ITypeSymbol type)
   {
      Namespace = type.ContainingNamespace.IsGlobalNamespace ? null : type.ContainingNamespace.ToDisplayString();
      Name = type.Name;
      PropertyNames = GetPropertyNames(type);
   }

   private static ImmutableArray<IPropertySymbol> GetPropertyNames(ITypeSymbol type)
   {
      return type.GetMembers()
         .Select(m =>
         {
            // Only properties
            if (m is not IPropertySymbol prop || m.DeclaredAccessibility != Accessibility.Public)
               return null;

            // Without ignore attribute
            if (GenHelper.IsPropsToStringIgnore(m))
               return null;

            return (IPropertySymbol)m;
            //return SymbolEqualityComparer.Default.Equals(prop.Type, type) ? prop.Name : null;
         })
         .Where(m => m != null)!
         .ToImmutableArray<IPropertySymbol>();
   }

   public override bool Equals(object? obj) => obj is ClassInfo other && Equals(other);

   public bool Equals(ClassInfo other)
   {
      return Namespace == other.Namespace
         && Name == other.Name
         && PropertyNames.SequenceEqual(other.PropertyNames); //  <-- Problem Line
   }

   public override int GetHashCode()
   {
      var hashCode = (Namespace != null ? Namespace.GetHashCode() : 0);
      hashCode = (hashCode * 397) ^ Name.GetHashCode();
      hashCode = (hashCode * 397) ^ ((IStructuralEquatable)PropertyNames).GetHashCode(EqualityComparer<IPropertySymbol>.Default); //  <-- Problem Line

      return hashCode;
   }
}