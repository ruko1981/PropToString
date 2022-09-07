using global::Microsoft.CodeAnalysis;
using global::Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PropToString;
internal static class GenHelper
{
   internal const string PTSNamespace = "PropToString";
   internal const string ClassAttributeName = "PropsToString";
   internal const string PropertyAttributeName = "PropToString";
   internal const string PropertyIgnoreAttributeName = "PropsToStringIgnore";

   internal const string AutoGenPreamble = @"// Auto Generated Source Code
// Author : Michael Diehl";


   internal static ClassInfo? GetClassInfoOrNull(GeneratorSyntaxContext context, CancellationToken cancellationToken)
   {
      // We know the node is a ClassDeclarationSyntax thanks to IsSyntaxTargetForGeneration
      var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

      var type = ModelExtensions.GetDeclaredSymbol(context.SemanticModel, classDeclarationSyntax, cancellationToken) as ITypeSymbol;

      return IsPropsToString(type) ? new ClassInfo(type!) : null;
   }

   private static string? GetAttributeName(NameSyntax? name)
   {
      return name switch
      {
         SimpleNameSyntax ins => ins.Identifier.Text,
         QualifiedNameSyntax qns => qns.Right.Identifier.Text,
         _ => null
      };
   }

   public static bool IsPropsToString(ISymbol? type)
   {
      return type is not null &&
             type.GetAttributes()
                 .Any(a => a.AttributeClass is
                 {
                    Name: ClassAttributeName,
                    ContainingNamespace:
                    {
                       Name: PTSNamespace,
                       ContainingNamespace.IsGlobalNamespace: true
                    }
                 });
   }

   public static bool IsPropsToStringIgnore(ISymbol type)
   {
      return type is not null &&
             type.GetAttributes()
                 .Any(a => a.AttributeClass is
                 {
                    Name: PropertyIgnoreAttributeName,
                    ContainingNamespace:
                    {
                       Name: PTSNamespace,
                       ContainingNamespace.IsGlobalNamespace: true
                    }
                 });
   }

   public static bool IsPropToString(ISymbol type)
   {
      return type is not null &&
             type.GetAttributes()
                 .Any(a => a.AttributeClass is
                 {
                    Name: PropertyAttributeName,
                    ContainingNamespace:
                    {
                       Name: PTSNamespace,
                       ContainingNamespace.IsGlobalNamespace: true
                    }
                 });
   }
}