using System;
using System.Collections.Generic;
using Xunit;

namespace Adom.Framework.Tests
{
    public class StringExtensionsTest
    {
        [InlineData("denehd839_ddiefnryboaoeifioencj__-dgeyxbbxejlaked eh", "~" )]
        [InlineData("_________________________________________________", "-")]
        [InlineData("azertyuiopqsdfghjklmwxcvbn,;:!", "#" )]
        [InlineData("aqwzsxedcrfvtgbyhnuj,ik;ol:pm!-dgeyxbbxejlaked eh", "$")]
        [InlineData("qsdfghjklm-", "-")]
        [Theory]
        public void SplitLineShouldReturnOnLine(string str, string separator)
        {
            List<string> splitedLines = new List<string>();
            foreach (SplitedLine line in str.SplitLine(separator))
            {
                splitedLines.Add(line.Line.ToString());
            }

            Assert.True(splitedLines.Count == 1);
        }

        [InlineData("azertyuiop-qsdfghjklm", "azertyuiop", "qsdfghjklm", "-")]
        [InlineData("qsdfghjklm-qsdfghjklm", "qsdfghjklm", "qsdfghjklm", "-")]
        [InlineData("qsdfghjklm-wxcvbn,;:!", "qsdfghjklm", "wxcvbn,;:!", "-")]
        [InlineData("1234567890)=-$^poiuytreza", "1234567890)=", "$^poiuytreza", "-")]
        [Theory]
        public void SplitLineShouldReturnLineSplited(string str, string line1, string line2, string separator)
        {
            List<string> splitedLines = new List<string>();
            foreach (SplitedLine line in str.SplitLine(separator))
            {
                splitedLines.Add(line.Line.ToString());
            }

            Assert.True(splitedLines.Count == 2);
            Assert.Equal(line1, splitedLines[0]);
            Assert.Equal(line2, splitedLines[1]);
        }

        [InlineData("azertyuiop-qsdfghjklm")]
        [InlineData("qsdfghjklm-qsdfghjklm")]
        [InlineData("qsdfghjklm-wxcvbn,;:!")]
        [InlineData("1234567890)=-$^poiuytreza")]
        [InlineData("1234567890")]
        [Theory]
        public void StringIsGuidShouldReturnFalse(string str)
        {
            Assert.False(str.IsGuid());
        }

        [InlineData("21d9d62e-ec1c-4ff5-857c-d9348c2039b0")]
        [InlineData("22d9d62e-ec1c-4ff5-857c-d9348c2039b1")]
        [InlineData("23d9d62e-ec1c-4ff5-857c-d9348c2039b2")]
        [InlineData("24d9d62e-ec1c-4ff5-857c-d9348c2039b3")]
        [InlineData("25d9d62e-ec1c-4ff5-857c-d9348c2039b4")]
        [InlineData("26d9d62e-ec1c-4ff5-857c-d9348c2039b5")]
        [InlineData("27d9d62e-ec1c-4ff5-857c-d9348c2039b6")]
        [InlineData("28d9d62e-ec1c-4ff5-857c-d9348c2039b7")]
        [InlineData("29d9d62e-ec1c-4ff5-857c-d9348c2039b8")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [Theory]
        public void StringIsGuidShouldReturnTrue(string str)
        {
            Assert.True(str.IsGuid());
        }

        [InlineData("10000000-0000-0000-0000-000000000000")]
        [InlineData("00000000-0100-0000-0000-000000000000")]
        [InlineData("00000000-0000-0010-0000-000000000000")]
        [InlineData("00000000-0000-0000-0001-000000010000")]
        [InlineData("0a000000-0000-0000-0000-000000000000")]
        [InlineData("00000000-0b00-0000-0000-000000000000")]
        [InlineData("00000000-0000-0c00-0000-000000000000")]
        [InlineData("00000000-0000-0000-00d0-000000000000")]
        [Theory]
        public void StringIsGuidEmptyShouldReturnFalse(string str)
        {
            Assert.False(str.IsGuidEmpty());
        }

        [Fact]
        public void StringIsGuidEmptyShouldReturnTrue()
        {
            string emptyGuid = Guid.Empty.ToString();
            Assert.True(emptyGuid.IsGuidEmpty());
        }
    }
}
