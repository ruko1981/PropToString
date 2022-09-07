using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace PropToString;

[Generator]
public class PropToStringGenerator : IIncrementalGenerator
{


   private static int _counter;

   public void Initialize(IncrementalGeneratorInitializationContext context)
   {

//#if DEBUG
//      if (!Debugger.IsAttached)
//      {
//         Debugger.Launch();
//      }
//#endif

      context.RegisterPostInitializationOutput(ctx =>
      {
         ctx.AddSource("PropsToStringAttribute.g.cs", SourceText.From(AttributeTexts.PropsToStringAttribute, Encoding.UTF8));
         ctx.AddSource("PropToStringAttribute.g.cs", SourceText.From(AttributeTexts.PropToStringAttribute, Encoding.UTF8));
         ctx.AddSource("PropsToStringIgnoreAttribute.g.cs", SourceText.From(AttributeTexts.PropsToStringIgnoreAttribute, Encoding.UTF8));
      });


      var classProvider = context.SyntaxProvider
          .CreateSyntaxProvider(
              static (node, _) => node is ClassDeclarationSyntax { AttributeLists.Count: > 0 }, // select classes with attributes
              static (ctx, ct) => GenHelper.GetClassInfoOrNull(ctx, ct)
              )
          .Where(type => type is not null)
          .Collect()
          .SelectMany((classes, _) => classes.Distinct());

      context.RegisterSourceOutput(classProvider, Generate);

   }

   private static void Generate(SourceProductionContext context, ClassInfo? classInfo)
   {
      if (classInfo is null)
         throw new ArgumentNullException(nameof(classInfo));

      context.CancellationToken.ThrowIfCancellationRequested();
      var ns = classInfo?.Namespace;
      var name = classInfo?.Name;

      StringBuilder sb = new();
      foreach (var prop in classInfo?.PropertyNames)
      {
         sb.Append($@"-metadata {prop.Name}={{JsonSerializer.Serialize({prop.Name})}} ");
      }

      //TODO : Only include namespace is "ns" is not null
      var genText = $@"{GenHelper.AutoGenPreamble}

Counter = {_counter++}

using global::System.Text.Json;

namespace {ns}
{{
   public partial class {name}
   {{
      public string ToPropertyString() => $""{sb.ToString().Trim()}"";
   }}
}}";

      context.AddSource($"{name}.g.cs", SourceText.From(genText, Encoding.UTF8));
   }
}