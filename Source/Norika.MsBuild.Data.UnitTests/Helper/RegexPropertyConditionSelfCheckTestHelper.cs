using System.Text.RegularExpressions;
using Norika.MsBuild.Core.Data;

namespace Norika.MsBuild.Data.UnitTests.Helper
{
    internal class RegexPropertyConditionSelfCheckTestHelper
    {
        private static readonly RegexFactory Factory = new RegexFactory();

        /// <summary>
        /// Returns a regex for checking the given properties condition for a empty-self-check
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        internal static Regex GetSystemUnderTestForSelfCheckIsEmpty(string propertyName)
        {
            return Factory.CreatePropertyConditionSelfCheckEmptyRegex(propertyName);
        }

        /// <summary>
        /// Returns a regex for checking the given properties condition for a not-empty-self-check
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        internal static Regex GetSystemUnderTestForSelfCheckIsNotEmpty(string propertyName)
        {
            return Factory.CreatePropertyConditionSelfCheckIsNotEmptyEmptyRegex(propertyName);
        }
    }
}
