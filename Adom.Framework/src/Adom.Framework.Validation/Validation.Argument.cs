using System;

namespace Adom.Framework.Validation
{
    public sealed partial class Argument
    {
        [ThreadStatic]
        private const CheckLevel LEVEL = CheckLevel.Argument;
    }
}
