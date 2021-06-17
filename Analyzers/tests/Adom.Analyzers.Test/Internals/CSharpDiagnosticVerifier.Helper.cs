namespace Adom.Analyzers.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Diagnostics;
    using Microsoft.CodeAnalysis.Text;

    public class DiagnosticVerifier
    {
        internal const string DEFAULT_FILE_PATH_PREFIX = "Test";
        internal const string CSHARP_DEFAULT_FILE_EXT = "cs";
        internal const string TEST_PROJECT_NAME = "TestProject";
    }

    /// <summary>
    /// Class for turning strings into documents and getting the diagnostics on them
    /// All methods are static
    /// </summary>
    public abstract partial class CSharpDiagnosticVerifier<T> : DiagnosticVerifier where T : DiagnosticAnalyzer, new()
    {
        private static readonly MetadataReference _corlibReference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
        private static readonly MetadataReference _systemCoreReference = MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location);
        private static readonly MetadataReference _cSharpSymbolsReference = MetadataReference.CreateFromFile(typeof(CSharpCompilation).Assembly.Location);
        private static readonly MetadataReference _codeAnalysisReference = MetadataReference.CreateFromFile(typeof(Compilation).Assembly.Location);

        #region  Get Diagnostics

        /// <summary>
        /// Given classes in the form of strings, their language, and an IDiagnosticAnlayzer to apply to it, return the diagnostics found in the string after converting it to a document.
        /// </summary>
        /// <param name="sources">Classes in the form of strings</param>
        /// <param name="language">The language the source classes are in</param>
        /// <param name="analyzer">The analyzer to be run on the sources</param>
        /// <returns>An IEnumerable of Diagnostics that surfaced in the source code, sorted by Location</returns>
        private static Diagnostic[] GetSortedDiagnostics(TestSourceFile[] sources, string language, DiagnosticAnalyzer analyzer)
        {
            return GetSortedDiagnosticsFromDocuments(analyzer, GetDocuments(sources, language));
        }

        /// <summary>
        /// Given an analyzer and a document to apply it to, run the analyzer and gather an array of diagnostics found in it.
        /// The returned diagnostics are then ordered by location in the source document.
        /// </summary>
        /// <param name="analyzer">The analyzer to run on the documents</param>
        /// <param name="documents">The Documents that the analyzer will be run on</param>
        /// <returns>An IEnumerable of Diagnostics that surfaced in the source code, sorted by Location</returns>
        protected static Diagnostic[] GetSortedDiagnosticsFromDocuments(DiagnosticAnalyzer analyzer, Document[] documents)
        {
            var projects = new HashSet<Project>();
            if (documents == null || !documents.Any())
            {
                throw new ArgumentNullException(nameof(documents));
            }

            foreach (var document in documents)
            {
                projects.Add(document.Project);
            }

            var documentTrees = documents.Select(document => document.GetSyntaxTreeAsync().Result).ToImmutableHashSet();

            var diagnostics = new List<Diagnostic>();
            foreach (var project in projects)
            {
                var compilationWithAnalyzers = project.GetCompilationAsync().Result.WithAnalyzers(ImmutableArray.Create(analyzer));
                var diags = compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().Result;
                var documentDiags = diags.Where(
                    diag => diag.Location == Location.None || diag.Location.IsInMetadata || documentTrees.Contains(diag.Location.SourceTree));
                diagnostics.AddRange(documentDiags);
            }

            var results = SortDiagnostics(diagnostics);
            diagnostics.Clear();
            return results;
        }

        /// <summary>
        /// Sort diagnostics by location in source document
        /// </summary>
        /// <param name="diagnostics">The list of Diagnostics to be sorted</param>
        /// <returns>An IEnumerable containing the Diagnostics in order of Location</returns>
        private static Diagnostic[] SortDiagnostics(IEnumerable<Diagnostic> diagnostics)
        {
            return diagnostics.OrderBy(d => d.Location.SourceSpan.Start).ToArray();
        }

        #endregion

        #region Set up compilation and documents

        /// <summary>
        /// Given an array of strings as sources and a language, turn them into a project and return the documents and spans of it.
        /// </summary>
        /// <param name="sources">Classes in the form of strings</param>
        /// <param name="language">The language the source code is in</param>
        /// <returns>A Tuple containing the Documents produced from the sources and their TextSpans if relevant</returns>
        private static Document[] GetDocuments(TestSourceFile[] sources, string language)
        {
            if (language != LanguageNames.CSharp && language != LanguageNames.VisualBasic)
            {
                throw new ArgumentException("Unsupported Language");
            }

            var project = CreateProject(sources, language);
            var documents = project.Documents.ToArray();

            if (sources.Length != documents.Length)
            {
#pragma warning disable CA2201 // Ne pas lever de types d'exception réservés
                throw new SystemException("Amount of sources did not match amount of Documents created");
#pragma warning restore CA2201 // Ne pas lever de types d'exception réservés
            }

            return documents;
        }

        /// <summary>
        /// Create a Document from a string through creating a project that contains it.
        /// </summary>
        /// <param name="source">Classes in the form of a string</param>
        /// <param name="language">The language the source code is in</param>
        /// <returns>A Document created from the source string</returns>
        protected static Document CreateDocument(TestSourceFile source, string language = LanguageNames.CSharp)
        {
            return CreateProject(new[] { source }, language).Documents.First();
        }

        /// <summary>
        /// Ensures that a project with a given name exists in the solution, and creates it if it does not exist
        /// </summary>
        /// <param name="solution">the solution in which to look for the project</param>
        /// <param name="projectName">the name of the project we want to ensure it is present</param>
        /// <param name="language">the language of the project</param>
        /// <param name="projectId">the Id of the ensured project</param>
        /// <returns>the solution, containing the project if needed to be related</returns>
        private static Solution EnsureProject(Solution solution, string projectName, string language, out ProjectId projectId)
        {
            var proj =
                solution.Projects.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));
            if (proj == null)
            {
                projectId =
                    ProjectId.CreateNewId(
                        debugName: string.IsNullOrWhiteSpace(projectName) ? TEST_PROJECT_NAME : projectName);
                solution = solution.AddProject(projectId, projectName, projectName, language);
            }
            else
            {
                projectId = proj.Id;
            }
            return solution;
        }

        /// <summary>
        /// Create a project using the inputted strings as sources.
        /// </summary>
        /// <param name="sources">Classes in the form of strings</param>
        /// <param name="language">The language the source code is in</param>
        /// <returns>A Project created out of the Documents created from the source strings</returns>
        private static Project CreateProject(TestSourceFile[] sources, string language = LanguageNames.CSharp)
        {
            var projectName = sources.Select(s => s.ProjectName).FirstOrDefault();
            projectName = string.IsNullOrWhiteSpace(projectName) ? TEST_PROJECT_NAME : projectName;

            string fileNamePrefix = DEFAULT_FILE_PATH_PREFIX;
            string fileExt = CSHARP_DEFAULT_FILE_EXT;

            var projectId = ProjectId.CreateNewId(debugName: projectName);

            using (var workspace = new AdhocWorkspace())
            {
                var solution = workspace.CurrentSolution
                    .AddProject(projectId, projectName, projectName, language)
                    .AddMetadataReference(projectId, _corlibReference)
                    .AddMetadataReference(projectId, _systemCoreReference)
                    .AddMetadataReference(projectId, _cSharpSymbolsReference)
                    .AddMetadataReference(projectId, _codeAnalysisReference);
                int count = 0;
                foreach (var source in sources)
                {
                    var newFileName = string.IsNullOrWhiteSpace(source.FileName) ? fileNamePrefix + count + "." + fileExt : source.FileName;
                    var documentId = DocumentId.CreateNewId(projectId, debugName: newFileName);
                    solution =
                        solution.AddDocument(documentId, newFileName, SourceText.From(source))
                            .AddMetadataReferences(projectId, source.MetadataReferences);
                    foreach (var referencedProjectName in source.ProjectReferences)
                    {
                        ProjectId referencedProjectId;
                        solution = EnsureProject(solution, referencedProjectName, language, out referencedProjectId);
                        solution = solution.AddProjectReferences(projectId, new[] { new ProjectReference(referencedProjectId) });
                    }

                    count++;
                }

                return solution.GetProject(projectId);
            }
        }

        #endregion
    }
}
