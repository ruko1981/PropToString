namespace PropToString;
internal class AttributeTexts
{
   // TODO : Impliment class-wide string template
   // TODO : Impliment per-property string template
   // TODO : Impliment per-property newline

   public const string PropsToStringAttribute = $@"{GenHelper.AutoGenPreamble}

using global::System;

namespace {GenHelper.PTSNamespace}
{{
   [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
   sealed class {GenHelper.ClassAttributeName} : Attribute
   {{
      public string Template {{get; set; }} = string.Empty;
      
      public {GenHelper.ClassAttributeName}()
      {{
      }}
   }}
}}
";

   public const string PropToStringAttribute = $@"{GenHelper.AutoGenPreamble}

using global::System;

namespace {GenHelper.PTSNamespace}
{{
   [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
   [System.Diagnostics.Conditional(""PropsToStringGenerator_DEBUG"")]
   sealed class {GenHelper.PropertyAttributeName} : Attribute
   {{
      public string DisplayName {{ get; set; }};
      public string Template {{ get; set; }} = string.Empty;
      public bool NewLine {{get; set;}} = false;

      public {GenHelper.PropertyAttributeName}()
      {{
         DisplayName = string.Empty
      }}

      public {GenHelper.PropertyAttributeName}(string displayName)
      {{
         DisplayName = displayName;
      }}
   }}
}}
";

   public const string PropsToStringIgnoreAttribute = $@"{GenHelper.AutoGenPreamble}

using global::System;

namespace {GenHelper.PTSNamespace}
{{
   [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
   [System.Diagnostics.Conditional(""PropsToStringGenerator_DEBUG"")]
   sealed class {GenHelper.PropertyIgnoreAttributeName} : Attribute
   {{
       public {GenHelper.PropertyIgnoreAttributeName}()
       {{
       }}
   }}
}}
";
}
