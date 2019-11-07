using Norika.MsBuild.Model.Interfaces;
using Norika.MsBuild.Model.Types;

namespace Norika.MsBuild.Core.Data.Converter
{
    /// <summary>
    /// Provides creation logic for a msbuild converter
    /// </summary>
    public class MsBuildConverterFactory
    {
        /// <summary>
        /// Creates a converter of the given type
        /// </summary>
        /// <typeparam name="T">Target type for which the converter should be created</typeparam>
        /// <returns>A new instance of a converter</returns>
        public virtual IMsBuildConverter<T> CreateConverter<T>() where T : struct
        {
            if (typeof(T) == typeof(ContinueOnError))
            {
                return (IMsBuildConverter<T>) new MsBuildStringToContinueOnErrorConverter();
            }

            return default(IMsBuildConverter<T>);
        }
    }
}