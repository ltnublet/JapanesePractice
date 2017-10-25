// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style",
    "IDE0016:Use 'throw' expression",
    Justification = "Cannot be simplified due to additional behaviour when supplied IEnumerable contains no elements.",
    Scope = "member",
    Target = "~M:JapanesePractice.Contract.Interpretations.Interpretation`1.#ctor(System.Collections.Generic.IEnumerable{`0})")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.Usage",
    "CA2214:DoNotCallOverridableMethodsInConstructors",
    Justification = "IEnumerable<T> is not compatible with params T[], so we simulate it by creating an overloaded constructor.",
    Scope = "member",
    Target = "JapanesePractice.Contract.Interpretations.Interpretation`1.#.ctor(System.Collections.Generic.IEnumerable`1<!0>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.Usage",
    "CA2214:DoNotCallOverridableMethodsInConstructors",
    Justification = "IEnumerable<T> is not compatible with params T[], so we simulate it by creating an overloaded constructor.",
    Scope = "member",
    Target = "JapanesePractice.Contract.Interpretations.Interpretation`1.#.ctor(!0[])")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.Naming",
    "CA1710:IdentifiersShouldHaveCorrectSuffix",
    Justification = "Class exposes additional functionality besides being a collection.",
    Scope = "type",
    Target = "JapanesePractice.Contract.Category")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.Design",
    "CA1024:UsePropertiesWhereAppropriate",
    Justification = "The method is named `GetPermittedRepresentations` because that's what it does; however, it is not a property.",
    Scope = "member",
    Target = "JapanesePractice.Contract.Interpretations.IInterpretation.#GetPermittedInterpretations()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.Design",
    "CA1026:DefaultParametersShouldNotBeUsed",
    Justification = "Callers which do not support default parameters can refer to the documentation for safe defaults.",
    Scope = "member",
    Target = "JapanesePractice.Contract.Interpretations.InterpretationCollection.#Compare(JapanesePractice.Contract.Interpretations.InterpretationCollection,System.Func`3<JapanesePractice.Contract.Interpretations.IInterpretation,JapanesePractice.Contract.Interpretations.IInterpretation,System.Boolean>)")]