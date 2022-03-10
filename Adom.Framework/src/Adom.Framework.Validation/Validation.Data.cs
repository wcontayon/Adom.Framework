using System;

namespace Adom.Framework.Validation
{
    public sealed partial class Data
    {
        [ThreadStatic]
        private const CheckLevel LEVEL = CheckLevel.Data;
    }
}
