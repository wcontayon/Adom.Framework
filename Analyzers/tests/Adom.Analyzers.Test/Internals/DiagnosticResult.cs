namespace Adom.Analyzers.Test 
{
    using System;
    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Location where the diagnostic appears, as determined by path, line number, and column number.
    /// </summary>
    public struct DiagnosticResultLocation 
    {
        public string Path { get; }

        public int Line { get; }

        public int Column { get; }

        public DiagnosticResultLocation(string path, int line, int column)
        {
            if (line < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(line), "line must be >= -1");
            }

            if (column < -1){
                throw new ArgumentOutOfRangeException(nameof(column), "column must be >= -1");
            }

            this.Path = path;
            this.Line = line;
            this.Column = column;
        }
    }

    /// <summary>
    /// Struct that stores information about a Diagnostic appearing in a source
    /// </summary>
    public struct DiagnosticResult
    {
        private DiagnosticResultLocation[] locations;

        public DiagnosticResultLocation[] Locations 
        {
            get 
            {
                if (this.locations == null)
                {
                    this.locations = Array.Empty<DiagnosticResultLocation>();
                }

                return this.locations;
            }
            set
            {
                this.locations = value;
            }
        }

        public DiagnosticSeverity Severity { get; set; }

        public string Id { get; set; }

        public string Message { get; set; }

        public string Path => this.Locations.Length > 0 ? this.Locations[0].Path : string.Empty;

        public int Line => this.Locations.Length > 0 ? this.Locations[0].Line : -1;

        public int Column => this.Locations.Length > 0 ? this.Locations[0].Column : -1;

    }

    public static class LocationHelper
    {
        public static DiagnosticResultLocation[] InTestFile(int line, int column)
        {
            return new[]
                       {
                           new DiagnosticResultLocation(
                               DiagnosticVerifier.DEFAULT_FILE_PATH_PREFIX + "0." + DiagnosticVerifier.CSHARP_DEFAULT_FILE_EXT,
                               line,
                               column),
                       };
        }
    }

}