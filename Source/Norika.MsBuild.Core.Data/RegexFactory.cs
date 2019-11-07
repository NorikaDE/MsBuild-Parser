using System.Text.RegularExpressions;

namespace Norika.MsBuild.Core.Data
{
    public class RegexFactory
    {
        private static string _propertyConditionSelfCheckEmptyRegexPattern =
            "(?'PropertyQuote'\\'|(?<!\\'))\\$\\(<PropertyName>\\)(\\k'PropertyQuote')(\\s){0,2}\\=\\=(\\s){0,2}\\'(\\s){0,1}\\'";

        private static string _propertyConditionSelfCheckIsNotEmptyRegexPattern =
            "(?'PropertyQuote'\\'|(?<!\\'))\\$\\(<PropertyName>\\)(\\k'PropertyQuote')(\\s){0,2}\\!\\=(\\s){0,2}\\'(\\s){0,1}\\'";
                   
        public Regex CreatePropertyConditionSelfCheckEmptyRegex(string propertyName)
        {
            string propertySelfCheckPattern =
                _propertyConditionSelfCheckEmptyRegexPattern.Replace("<PropertyName>", propertyName);

            return new Regex(propertySelfCheckPattern);
        }

        public Regex CreatePropertyConditionSelfCheckIsNotEmptyEmptyRegex(string propertyName)
        {
            string propertySelfCheckPattern =
                _propertyConditionSelfCheckIsNotEmptyRegexPattern.Replace("<PropertyName>", propertyName);

            return new Regex(propertySelfCheckPattern);
        }
    }
}
