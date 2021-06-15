namespace Adom.Analyzers {

    using System.Globalization;

    internal class LinkUriHelper {

        public static string GetHelpLinkUri(string ruleIdentifier)
            => string.Format(
                CultureInfo.InvariantCulture,
                "https://github.com/wcontayon/Adom.Framework/Analyzers/blob/docs/rules/{0}",
                ruleIdentifier);

    }

}