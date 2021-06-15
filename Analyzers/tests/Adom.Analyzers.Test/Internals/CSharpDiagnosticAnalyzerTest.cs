namespace Adom.Analyzers.Test {

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    internal abstract class CSharpDiagnosticAnalyzerTest<T> where T : DiagnosticAnalyzer, new() {

        private readonly DiagnosticAnalyzer _diagnosticAnalyzer;

    }

}