// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style",
    "IDE0016:Use 'throw' expression",
    Justification = "Cannot be simplified due to additional behaviour when supplied IEnumerable contains no elements.",
    Scope = "member",
    Target = "~M:JapanesePractice.Interpretations.Interpretation`1.#ctor(System.Collections.Generic.IEnumerable{`0})")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.Usage",
    "CA2214:DoNotCallOverridableMethodsInConstructors",
    Justification = "IEnumerable<T> is not compatible with params T[], so we simulate it by creating an overloaded constructor.",
    Scope = "member",
    Target = "JapanesePractice.Interpretations.Interpretation`1.#.ctor(System.Collections.Generic.IEnumerable`1<!0>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.Usage",
    "CA2214:DoNotCallOverridableMethodsInConstructors",
    Justification = "IEnumerable<T> is not compatible with params T[], so we simulate it by creating an overloaded constructor.",
    Scope = "member",
    Target = "JapanesePractice.Interpretations.Interpretation`1.#.ctor(!0[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.Usage",
    "CA2202:Do not dispose objects multiple times",
    Justification = "This is triggered by the nested usings. The inner using nominally disposes of the TextReader when the JsonReader is disposed; however, we set the JsonReader's CloseInput property to false at runtime, which resolves the issue.",
    Scope = "member",
    Target = "JapanesePractice.Contexts.TextualContext.#FromFile(System.String)")]