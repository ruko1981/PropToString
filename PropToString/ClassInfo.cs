using Microsoft.CodeAnalysis;
using System.Collections;
using System.Collections.Immutable;
using System.Text;

namespace PropToString;
public readonly struct ClassInfo : IEquatable<ClassInfo>
{
   public readonly string? Namespace { get; }
   public readonly string Name { get; }
   public readonly ImmutableArray<(string DisplayName, string PropName)> PropertyNames { get; }

   public ClassInfo(ITypeSymbol type)
   {
      Namespace = type.ContainingNamespace.IsGlobalNamespace ? null : type.ContainingNamespace.ToDisplayString();
      Name = type.Name;
      PropertyNames = GetPropertyNames(type);
   }

   private static ImmutableArray<(string DisplayName, string PropName)> GetPropertyNames(ITypeSymbol type)
   {
      string displayName, propName;

      return type.GetMembers()
         .Select(m =>
            {
               // Only properties
               if (m is not IPropertySymbol prop || m.DeclaredAccessibility != Accessibility.Public)
                  return (null, null);

               // Without ignore attribute
               if (GenHelper.IsPropsToStringIgnore(m))
                  return (null, null);

               displayName = m.GetAttributes()
                   .FirstOrDefault(a => a.AttributeClass is
                   {
                      Name: GenHelper.PropertyAttributeName,
                      ContainingNamespace:
                      {
                         Name: GenHelper.PTSNamespace,
                         ContainingNamespace.IsGlobalNamespace: true
                      }
                   })?
                   .NamedArguments
                   .FirstOrDefault(p => p.Key == "DisplayName")
                   .Value.Value as string ?? m.Name;

               propName = m.Name;

               return (displayName, propName);
               //return d?.NamedArguments.First(p => p.Key == "DisplayName").Value.Value as string;


            })
         .Where(m => m != (null, null))!
         .ToImmutableArray<(string DisplayName, string PropName)>();
   }

   public override bool Equals(object? obj) => obj is ClassInfo other && Equals(other);

   public bool Equals(ClassInfo other)
   {
      if (ReferenceEquals(null, other))
         return false;

      return Namespace == other.Namespace
         && Name == other.Name
         && PropertyNames.SequenceEqual(other.PropertyNames);
   }

   public override int GetHashCode()
   {
      var hashCode = (Namespace != null ? Namespace.GetHashCode() : 0);
      hashCode = (hashCode * 397) ^ Name.GetHashCode();
      hashCode = (hashCode * 397) ^ PropertyNames.GetHashCode();

      return hashCode;
   }
}