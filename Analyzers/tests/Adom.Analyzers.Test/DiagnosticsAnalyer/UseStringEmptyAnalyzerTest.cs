namespace Adom.Analyzers.Test
{
    using Adom.Analyzers.Diagnostics;

    using Microsoft.CodeAnalysis;

    using Xunit;

    public class UseStringEmptyAnalyzerTest : CSharpDiagnosticVerifier<UseStringEmptyDiagnosticAnalyzer>
    {
        private static DiagnosticResult BuildExpectedDiagnostic(int line, int column)
        {
            return new DiagnosticResult
            {
                Id = RulesIds.UseStringEmpty,
                Message = AnalyzerDescriptions.UseStringEmptyAnalyzerDescription,
                Severity = DiagnosticSeverity.Warning,
                Locations = LocationHelper.InTestFile(line, column),
            };
        }

        [Fact]
        public void AD0001EmptyCodeNoDiagnosticsTriggered()
        {
            var test = string.Empty;
            this.VerifyCSharpDiagnostic(test);
        }

        [Fact]
        public void AD0001RuleVarDeclarationUseEmptyDiagnosticTriggererd()
        {
            var test = @"
                namespace Test
                {
                    class Test
                    {
                        void TestMethod()
                        {
                            var stringVal = \""\"";
                        }
                    }
                }";

            var diagnosticExpected = BuildExpectedDiagnostic(8, 29);
            this.VerifyCSharpDiagnostic(test, diagnosticExpected);
        }

        [Fact]
        public void AD0001RuleConditionOperationUseEmptyDiagnosticTriggererd()
        {
            var test = @"
                namespace Test
                {
                    class Test
                    {
                        void TestMethod()
                        {
                            var stringVal = ""fakeValue"";
                            if (stringVal == """")
                            {
                                Console.WriteLine(""no using string.empty"");
                            }
                        }
                    }
                }";

            var diagnosticExpected = BuildExpectedDiagnostic(9, 46);
            this.VerifyCSharpDiagnostic(test, diagnosticExpected);
        }

        [Fact]
        public void AD0001RuleVarDeclarationUseEmptyNoDiagnosticTriggererd()
        {
            var test = @"
                namespace Test
                {
                    class Test
                    {
                        void TestMethod()
                        {
                            var stringVal = string.Empty;
                        }
                    }
                }";

            this.VerifyCSharpDiagnostic(test);
        }

        [Fact]
        public void AD0001RuleConditionOperationUseEmptyNoDiagnosticTriggererd()
        {
            var test = @"
                namespace Test
                {
                    class Test
                    {
                        void TestMethod()
                        {
                            var stringVal = ""fakeValue"";
                            if (stringVal == string.Empty)
                            {
                                Console.WriteLine(""using string.empty"");
                            }
                        }
                    }
                }";

            this.VerifyCSharpDiagnostic(test);
        }
    }
}
