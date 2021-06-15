namespace Adom.Analyzers.Diagnostics {

    using System;
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Diagnostics;
    using Microsoft.CodeAnalysis.Operations;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class UseStringEmptyDiagnosticAnalyzer : DiagnosticAnalyzer {

        private static readonly DiagnosticDescriptor rule = new DiagnosticDescriptor(
                RulesIds.UseStringEmpty,
                AnalyzerTitles.UseStringEmptyAnalyzerTitles,
                AnalyzerDescriptions.UseStringEmptyAnalyzerDescription,
                RuleCategories.Style,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: string.Empty,
                helpLinkUri: LinkUriHelper.GetHelpLinkUri(RulesIds.UseStringEmpty));

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(rule);

        public override void Initialize(AnalysisContext context)
        {
            if (context == null){
                throw new ArgumentNullException(nameof(context));
            }

            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

            context.RegisterOperationAction(HandleComparaisonStringEmptyNotUsed, OperationKind.Conditional);
            context.RegisterSyntaxNodeAction(HandleVariableDeclarionStringEmptyNotUsed, SyntaxKind.VariableDeclaration);
        }

        private static void HandleComparaisonStringEmptyNotUsed(OperationAnalysisContext context){
            var operation = (IBinaryOperation)context.Operation;
            if (operation.OperatorKind == BinaryOperatorKind.Equals){
                var rightOperand = operation.RightOperand;
                if (rightOperand.Type.GetType() == typeof(string) &&
                    rightOperand.ConstantValue.HasValue &&
#pragma warning disable CA1820 // We are checking that the developer is not used ""
                    (string)rightOperand.ConstantValue.Value == "") {
#pragma warning restore CA1820 // We are checking that the developer is not used ""
                    context.ReportDiagnostic(Diagnostic.Create(rule, operation.Syntax.GetLocation()));
                }
            }
        }

        private static void HandleVariableDeclarionStringEmptyNotUsed(SyntaxNodeAnalysisContext context){
            var declaration = context.Node as VariableDeclarationSyntax;
            if (declaration != null){
                var predifinedType = declaration.Type;
                if (predifinedType.GetType() == typeof(string)){
                    var variableValue = declaration.Variables.First().Initializer.Value.GetFirstToken().Text;
#pragma warning disable CA1820 // We are checking that the developer is not used ""
                    if (variableValue == ""){
#pragma warning restore CA1820 // We are checking that the developer is not used ""
                        context.ReportDiagnostic(Diagnostic.Create(rule, declaration.GetLocation(), variableValue));
                    }
                }
            }
        }
    }
}