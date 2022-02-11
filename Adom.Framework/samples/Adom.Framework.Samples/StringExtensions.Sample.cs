using System;
using System.Collections.Generic;

namespace Adom.Framework.Samples
{
    public static class StringExtensions
    {
        public const string line1 = "azertyuiop-qsdfghjklm";
        public const string line2 = "qsdfghjklm-qsdfghjklm";
        public const string line3 = "qsdfghjklm-wxcvbn,;:!";
        public const string line4 = "1234567890)=-$^poiuytreza";
        public const string line5 = "1234567890";

        public static List<string> SplitedSample1()
        {
            List<string> lines = new List<string>();
            foreach (SplitedLine line in line1.SplitLine('~'))
            {
                lines.Add(line.Line.ToString());
            }

            return lines;
        }

        public static List<string> SplitedSample2()
        {
            List<string> lines = new List<string>();
            foreach (SplitedLine line in line2.SplitLine('-'))
            {
                lines.Add(line.Line.ToString());
            }

            return lines;
        }

        public static bool IsGuidFalse()
        {
            string fakeGuid = "djendeonde-bdehjbdkajbdkejbkebfr-abdbekjbfejfrfbrkb";
            return fakeGuid.IsGuid();
        }

        public static bool IsGuidTrue()
        {
            string trueGuid = Guid.NewGuid().ToString();
            return trueGuid.IsGuid();
        }
    }
}
