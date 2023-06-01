using System;

namespace Adom.Framework.Validation
{
    public sealed partial class Data
    {
        [ThreadStatic]
#pragma warning disable CA2019 // Improper 'ThreadStatic' field initialization
        private const CheckLevel LEVEL = CheckLevel.Data;
#pragma warning restore CA2019 // Improper 'ThreadStatic' field initialization
    }
}
