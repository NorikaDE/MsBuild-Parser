using System;

namespace Norika.MsBuild.Core.Data.Utilities
{
    public class MsBuildStringFormatProvider : IFormatProvider
    {
        public string MsBuildPropertyIdentifier = "$";
        public string MsBuildItemGroupIdentifier = "@";

        public string MsBuildVariableOpeningMask = "(";
        public string MsBuildVariableClosingMask = ")";

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return new MsBuildStringFormatter();
            }
            return null;
        }
    }
}