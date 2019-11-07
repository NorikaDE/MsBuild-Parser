using System;
using System.Text;

namespace Norika.MsBuild.Core.Data.Utilities
{
    public class MsBuildStringFormatter : ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!(formatProvider is MsBuildStringFormatProvider msBuildPropertyFormatProvider))
                return string.Empty;

            if (arg == null || string.IsNullOrWhiteSpace(arg.ToString()))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(format))
                return arg.ToString();
            
            return FormatString(format, arg, msBuildPropertyFormatProvider);
        }

        private static string FormatString(string format, object arg, MsBuildStringFormatProvider msBuildPropertyFormatProvider)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (format.Equals("Property"))
            {
                stringBuilder.Append(msBuildPropertyFormatProvider.MsBuildPropertyIdentifier);
            }

            if (format.Equals("ItemGroup"))
            {
                stringBuilder.Append(msBuildPropertyFormatProvider.MsBuildItemGroupIdentifier);
            }

            stringBuilder.Append(msBuildPropertyFormatProvider.MsBuildVariableOpeningMask);
            stringBuilder.Append(arg);
            stringBuilder.Append(msBuildPropertyFormatProvider.MsBuildVariableClosingMask);

            return stringBuilder.ToString();
        }
    }
}
