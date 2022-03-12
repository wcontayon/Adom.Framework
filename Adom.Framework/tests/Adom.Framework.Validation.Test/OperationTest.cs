using System;
using Xunit;

namespace Adom.Framework.Validation.Test
{
    /// <summary>
    /// Test class for <see cref="Operation"/>.
    /// </summary>
    public class OperationTest
    {
        private const string FALSE_MSG_EXCEPTION = "condition returned false";
        private const string TRUE_MSG_EXCEPTION = "condition return true";

        /// <summary>
        /// Fact.
        /// </summary>
        [Fact]
        public void OperationCheckShouldReturnWithoutThrowException()
        {
            // Act
            // Should not throw exception.
            Operation.ThrowIf(1 < 0, FALSE_MSG_EXCEPTION);

            // Assert
            Assert.True(Operation.False(1 < 0, FALSE_MSG_EXCEPTION, false));
            Assert.True(Operation.False(() => false, FALSE_MSG_EXCEPTION, false));

            Assert.True(Operation.True(1 > 0, TRUE_MSG_EXCEPTION, false));
            Assert.True(Operation.True(() => true, TRUE_MSG_EXCEPTION, false));

            Assert.True(Operation.That(1 > 0, false, TRUE_MSG_EXCEPTION));
            Assert.False(Operation.That(1 < 0, false, FALSE_MSG_EXCEPTION));
        }

        /// <summary>
        /// Fact.
        /// </summary>
        [Fact]
        public void OperationCheckShouldThrowExceptionWhenConditionIsFalse()
        {
            // Assert
            Assert.Throws<CheckException>(() => Operation.ThrowIf(1 > 0, TRUE_MSG_EXCEPTION));
            Assert.Throws<CheckException>(() => Operation.False(1 > 0, FALSE_MSG_EXCEPTION, true));
            Assert.Throws<CheckException>(() => Operation.False(() => true, FALSE_MSG_EXCEPTION, true));

            Assert.Throws<CheckException>(() => Operation.True(1 < 0, FALSE_MSG_EXCEPTION, true));
            Assert.Throws<CheckException>(() => Operation.True(() => false, FALSE_MSG_EXCEPTION, true));

            Assert.Throws<CheckException>(() => Operation.That(1 < 0, true, FALSE_MSG_EXCEPTION));
            Assert.Throws<CheckException>(() => Operation.That(1 < 0, true, FALSE_MSG_EXCEPTION));
        }

        /// <summary>
        /// Fact.
        /// </summary>
        [Fact]
        public void OperationCheckShouldNotThrowExceptionWhenConditionIsFalse()
        {
            // Assert
            Assert.False(Operation.False(1 > 0, FALSE_MSG_EXCEPTION, false));
            Assert.True(Operation.False(() => false, FALSE_MSG_EXCEPTION, false));

            Assert.True(Operation.True(1 > 0, TRUE_MSG_EXCEPTION, false));
            Assert.True(Operation.True(() => true, TRUE_MSG_EXCEPTION, false));

            Assert.True(Operation.That(1 > 0, false, TRUE_MSG_EXCEPTION));
            Assert.False(Operation.That(1 < 0, false, FALSE_MSG_EXCEPTION));
        }
    }
}
