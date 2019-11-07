using System;
using Norika.MsBuild.Model.Interfaces;
using Norika.MsBuild.Model.Types;

namespace Norika.MsBuild.Core.Data.Converter
{
    /// <summary>
    /// Implementation of a string to continue on error type converter
    /// </summary>
    public class MsBuildStringToContinueOnErrorConverter : IMsBuildConverter<ContinueOnError>
    {
        /// <summary>
        /// Parses the given string to ContinueOnError-enum
        /// </summary>
        /// <param name="s">To be parsed string</param>
        /// <returns>Enum value representing the given string</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws exception if the string value does not match a enum value</exception>
        public ContinueOnError Parse(string s)
        {
            string stringValue = s;
            
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return default(ContinueOnError);
            }
            
            if (bool.TryParse(s, out bool booleanValue))
            {
                stringValue =  (booleanValue ? 1 : 0).ToString();
            }
            if (Enum.TryParse(typeof(ContinueOnError), stringValue, true, out var value))
            {
                return (ContinueOnError) value;
            }
            throw new ArgumentOutOfRangeException($"The value '{s}' could not be converted to '{nameof(ContinueOnError)}'");
        }
    }
}