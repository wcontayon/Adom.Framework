namespace Adom.Analyzers.Test
{
    using Microsoft.CodeAnalysis;

    using System.Collections.Generic;

    /// <summary>
    /// A class dedicated to set-up test source files for test diagnostics.
    /// Its main interests being in its implicit conversion from and to string
    /// </summary>
    public class TestSourceFile
    {
        /// <summary>
        /// Gets or sets the name of the fake file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the source code to test. This property is the target from and to the string conversion.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the name of the project holding the file
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets the names of the projects which are referenced by this file's project
        /// </summary>
        public IList<string> ProjectReferences { get; } = new List<string>();

        /// <summary>
        /// Gets the collection of assembly references required to compile this file.
        /// </summary>
        public IList<MetadataReference> MetadataReferences { get; } = new List<MetadataReference>();

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Source;
        }

        /// <summary>
        /// conversion operator to String
        /// </summary>
        /// <param name="s">the object from which we will extract Source Property</param>
        public static implicit operator string(TestSourceFile s)
        {
            return s.ToString();
        }

        /// <summary>
        /// conversion operator from string
        /// </summary>
        /// <param name="s">the string which will be set as Source property of the TestSourceFile</param>
        public static implicit operator TestSourceFile(string s)
        {
            return TestSourceFile.FromString(s);
        }

        /// <summary>
        /// Convert a string as a TestSourceFile
        /// </summary>
        /// <param name="s">The name of the source file</param>
        public static TestSourceFile FromString(string s)
        {
            return new TestSourceFile { Source = s };
        }
    }
}
