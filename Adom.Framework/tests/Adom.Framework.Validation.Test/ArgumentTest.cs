using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Adom.Framework.Validation.Test
{
    public class ArgumentTest
    {
        private const string PARAMNAME = "FakeParam";

        private string s = string.Empty;
        private object? obj;
#pragma warning disable CA1805 // Ne pas effectuer d'initialisation inutilement
        private FakeClass? fake = default;
#pragma warning restore CA1805 // Ne pas effectuer d'initialisation inutilement

        /// <summary>
        /// Fact.
        /// </summary>
        [Fact]
        public void ArgumentCheckNotNullShouldThrowExceptionWhenNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => Argument.Require.NotNull<FakeClass>(null!, PARAMNAME));
            Assert.Throws<ArgumentNullException>(() => Argument.Require.NotNull<FakeClass>(this.fake, PARAMNAME));
            Assert.Throws<ArgumentNullException>(() => Argument.Require.NotNull(this.obj, PARAMNAME));
            Assert.Throws<ArgumentNullException>(() => Argument.Require.NotNullOrEmpty(this.s, PARAMNAME));

            Assert.Throws<ArgumentNullException>(() => Argument.Require.NotNullOrEmpty<FakeClass>(Enumerable.Empty<FakeClass>()));
            Assert.Throws<ArgumentNullException>(() => Argument.Require.NotNullOrEmpty<FakeClass>(null!));

            Assert.Throws<ArgumentNullException>(() => Argument.Require.NotNullOrEmpty(new List<FakeClass>()));

            Assert.ThrowsAny<Exception>(() => Argument.Assume.NotNull<FakeClass>(null!, PARAMNAME));
            Assert.ThrowsAny<Exception>(() => Argument.Assume.NotNull<FakeClass>(this.fake, PARAMNAME));
            Assert.ThrowsAny<Exception>(() => Argument.Assume.NotNull(this.obj, PARAMNAME));
            Assert.ThrowsAny<Exception>(() => Argument.Assume.NotNullOrEmpty(this.s, PARAMNAME));

            Assert.ThrowsAny<Exception>(() => Argument.Assume.NotNullOrEmpty<FakeClass>(Enumerable.Empty<FakeClass>()));
            Assert.ThrowsAny<Exception>(() => Argument.Assume.NotNullOrEmpty<FakeClass>(null!));

            Assert.ThrowsAny<Exception>(() => Argument.Assume.NotNullOrEmpty(new List<FakeClass>()));
        }

        /// <summary>
        /// Fact.
        /// </summary>
        [Fact]
        public void ArgumentCheckNotNullShouldNotThrowsException()
        {
            // Arrange
            this.obj = new { Fake = "em" };

            // Act
            var value = Argument.Require.NotNull<FakeClass>(new FakeClass(), PARAMNAME);
            var objval = Argument.Require.NotNull(this.obj, PARAMNAME);

            Argument.Require.NotNullOrEmpty("dedek", PARAMNAME);
            Argument.Require.NotNullOrEmpty("depdeinde\0", PARAMNAME);

            IEnumerable<FakeClass> col = new List<FakeClass>() { new FakeClass() };

            Argument.Require.NotNullOrEmpty(col.AsEnumerable());
            Argument.Require.NotNullOrEmpty(col.ToList());

            Assert.Equal(this.obj, objval);
            Assert.NotNull(value);
        }

        /// <summary>
        /// Fact.
        /// </summary>
        [Fact]
        public void ArgumentCheckNullShouldThrowExceptionWhenNotNull()
        {
            // Assert
            Assert.Throws<CheckException>(() => Argument.Require.Null<FakeClass>(new FakeClass(), PARAMNAME));
            Assert.Throws<CheckException>(() => Argument.Require.Null(this.obj = new object(), PARAMNAME));
            Assert.Throws<CheckException>(() => Argument.Require.NullOrEmpty("dkenldke", PARAMNAME));
            Assert.Throws<CheckException>(() => Argument.Require.NullOrEmpty("zkndlknlek\0", PARAMNAME));

            IEnumerable<FakeClass> col = new List<FakeClass>() { new FakeClass() };

            Assert.Throws<CheckException>(() => Argument.Require.NullOrEmpty<FakeClass>(col.AsEnumerable()));
            Assert.Throws<CheckException>(() => Argument.Require.NullOrEmpty<FakeClass>(col.ToList()));

            Assert.ThrowsAny<Exception>(() => Argument.Assume.Null<FakeClass>(new FakeClass(), PARAMNAME));
            Assert.ThrowsAny<Exception>(() => Argument.Assume.Null(this.obj = new object(), PARAMNAME));
            Assert.ThrowsAny<Exception>(() => Argument.Assume.NullOrEmpty("dkenldke", PARAMNAME));
            Assert.ThrowsAny<Exception>(() => Argument.Assume.NullOrEmpty("zkndlknlek\0", PARAMNAME));

            Assert.ThrowsAny<Exception>(() => Argument.Assume.NullOrEmpty<FakeClass>(col.AsEnumerable()));
            Assert.ThrowsAny<Exception>(() => Argument.Assume.NullOrEmpty<FakeClass>(col.ToList()));
        }

        /// <summary>
        /// Fact.
        /// </summary>
        [Fact]
        public void ArgumentCheckNullShouldNotThrowExceptionWhenNull()
        {
            Argument.Require.Null<FakeClass>(null!, PARAMNAME);
            Argument.Require.Null<FakeClass>(this.fake, PARAMNAME);
            Argument.Require.Null(this.obj, PARAMNAME);
            Argument.Require.NullOrEmpty(this.s, PARAMNAME);

            Argument.Require.NullOrEmpty<FakeClass>(Enumerable.Empty<FakeClass>());
            Argument.Require.NullOrEmpty<FakeClass>(null!);

            Argument.Require.NullOrEmpty(new List<FakeClass>());

            Argument.Assume.Null<FakeClass>(null!, PARAMNAME);
            Argument.Assume.Null<FakeClass>(this.fake, PARAMNAME);
            Argument.Assume.Null(this.obj, PARAMNAME);
            Argument.Assume.NullOrEmpty(this.s, PARAMNAME);

            Argument.Assume.NullOrEmpty<FakeClass>(Enumerable.Empty<FakeClass>());
            Argument.Assume.NullOrEmpty<FakeClass>(null!);

            Argument.Assume.NullOrEmpty(new List<FakeClass>());
        }
    }
}
