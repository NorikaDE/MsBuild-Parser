using System;
using System.Collections.Generic;

namespace Norika.MsBuild.Core.Data.Utilities
{
    /// <summary>
    /// Provides dedicated msbuild string operations 
    /// </summary>
    public static class MsBuildStringUtilities
    {
        private const char DefaultStringSeparator = ';';

        /// <summary>
        /// Splits a string by the msbuild default string separator
        /// </summary>
        /// <param name="s">The string that has to be split</param>
        /// <returns>Instance of IList containing the split string</returns>
        public static IList<string> SplitByDefaultSeparator(this string s)
        {
            return s.Split(DefaultStringSeparator, StringSplitOptions.RemoveEmptyEntries);
        }
        
        /// <summary>
        /// Default MsBuild string format provider
        /// </summary>
        public static readonly IFormatProvider FormatProvider = new MsBuildStringFormatProvider();
    }
}