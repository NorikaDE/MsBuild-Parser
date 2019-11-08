using System;
using System.Collections.Generic;
using System.Linq;

namespace Norika.MsBuild.Core.Data.Utilities
{
    /// <summary>
    /// Provides dedicated msbuild string operations 
    /// </summary>
    public static class MsBuildStringUtilities
    {
        /// <summary>
        /// Default msbuild line separator
        /// </summary>
        public const char DefaultLineSeparator = '\n';

        private const char DefaultStringSeparator = ';';

        /// <summary>
        /// Splits a string by the msbuild default string separator
        /// </summary>
        /// <param name="s">The string that has to be split</param>
        /// <returns>Instance of IList containing the split string</returns>
        public static IList<string> SplitByDefaultSeparator(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return new List<string>();

            return s.Split(DefaultStringSeparator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits the string by the default msbuild new line separator
        /// </summary>
        /// <param name="s">The string to separate</param>
        /// <returns>List of strings split by new line separator</returns>
        public static IList<string> SplitByNewLine(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return new List<string>();

            IList<string> lines = s.Split(DefaultLineSeparator, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            for (int i = 0; i < lines.Count; i++)
                lines[i] = lines[i].Trim();

            return lines;
        }

        /// <summary>
        /// Default MsBuild string format provider
        /// </summary>
        public static readonly IFormatProvider FormatProvider = new MsBuildStringFormatProvider();

        /// <summary>
        /// Replaces new line terminator with white space. Removes unnecessary
        /// white space. 
        /// </summary>
        /// <param name="inputValue">String that contains the new line terminators that should be replaced</param>
        /// <returns>Altered string</returns>
        public static string ReplaceNewLineWithSpace(string inputValue)
        {
            return string.Join(' ', SplitByNewLine(inputValue));
        }
    }
}