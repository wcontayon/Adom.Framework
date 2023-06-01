using System;

namespace Adom.Framework.Validation
{
    public sealed partial class Argument
    {
        [ThreadStatic]
#pragma warning disable CA2019 // Improper 'ThreadStatic' field initialization
        private const CheckLevel LEVEL = CheckLevel.Argument;
#pragma warning restore CA2019 // Improper 'ThreadStatic' field initialization
    }
}
