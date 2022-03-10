using Adom.Framework.ValuePath.Internals;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Adom.Framework.ValuePath
{
    public readonly struct ValuePath : IValuePathComparer
    {
        private readonly string? _valuePath;

        #region Internals constructor

        internal ValuePath(string path)
        {
            // check are made before assigning value to _valuePath
            Debug.Assert(path != null && !string.IsNullOrWhiteSpace(path));
            Debug.Assert(Path.IsPathFullyQualified(path));
            Debug.Assert(Path.GetFullPath(path) == path);

            _valuePath = path;
        }

        /// <summary>
        /// Returns a empty <see cref="ValuePath"/>
        /// </summary>
        public static ValuePath Empty => default;

        public bool IsEmpty => _valuePath is null;

        public string Value => _valuePath ?? "";

        public string Name => Path.GetFileName(_valuePath);

        public string NameWithoutExtension => Path.GetFileNameWithoutExtension(_valuePath);

        public string DirectoryName => Path.GetDirectoryName(_valuePath);

        public string Extension => Path.GetExtension(_valuePath);

        #endregion

        #region Operators

        public static bool operator ==(ValuePath left, ValuePath right) => left.Equals(right);
        public static bool operator !=(ValuePath left, ValuePath right) => !(left == right);

        public static bool Equals(ValuePath left, ValuePath right) => left._valuePath == right._valuePath;

        public int CompareTo(ValuePath other) => string.Compare(_valuePath, other._valuePath, RuntimeInformation.IsOSPlatform(OSPlatform.Windows), System.Globalization.CultureInfo.InvariantCulture);

        public bool Equals(ValuePath other) => this == other;

        public int CompareTo(object? obj)
        {
            if (obj is ValuePath path)
            {
                return CompareTo(path);
            }

            return -1;
        }

        public override bool Equals(object? obj)
        {
#pragma warning disable CA1065 // Ne pas lever d'exceptions dans les emplacements inattendus
            throw new NotImplementedException();
#pragma warning restore CA1065 // Ne pas lever d'exceptions dans les emplacements inattendus
        }

        public override int GetHashCode()
        {
#pragma warning disable CA1065 // Ne pas lever d'exceptions dans les emplacements inattendus
            throw new NotImplementedException();
#pragma warning restore CA1065 // Ne pas lever d'exceptions dans les emplacements inattendus
        }

        #endregion

        public static ValuePath GetTempPath() => FromPath(Path.GetTempPath());
        public static ValuePath GetTempFileName() => FromPath(Path.GetTempFileName());
        public static ValuePath GetFolderPath(Environment.SpecialFolder folder) => FromPath(Environment.GetFolderPath(folder));
        public static ValuePath CurrentDirectory() => FromPath(Environment.CurrentDirectory);

        #region Functions

        public static ValuePath FromPath(string path)
        {
            Debug.Assert(!string.IsNullOrEmpty(path));
            if (PathInternal.IsExtended(path))
            {
                path = path[4..];
            }

            var fullPath = Path.GetFullPath(path);
            var fullPathWithoutTrailingDirectorySeparator = Path.TrimEndingDirectorySeparator(fullPath);
            if (string.IsNullOrEmpty(fullPathWithoutTrailingDirectorySeparator))
            {
                return Empty;
            }

            return new ValuePath(path);
        }

        public static ValuePath FromFileSystemInfo(FileSystemInfo? fileSystemInfo)
        {
            if (fileSystemInfo != null && !string.IsNullOrEmpty(fileSystemInfo.FullName))
            {
                return FromPath(fileSystemInfo.FullName);
            }

            return Empty;
        }

        #endregion

        public static ValuePath Combine(ValuePath root, params string[] paths) => FromPath(Path.Combine(paths));

        public static bool operator <(ValuePath left, ValuePath right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(ValuePath left, ValuePath right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(ValuePath left, ValuePath right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(ValuePath left, ValuePath right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
