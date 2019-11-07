namespace Norika.MsBuild.Model.Interfaces
{
    /// <summary>
    /// Provides basic functionality for parsing string to msbuild specific
    /// formats. Target type have to be given.
    /// </summary>
    /// <typeparam name="T">Target type</typeparam>
    public interface IMsBuildConverter<out T> where T : struct
    {
        /// <summary>
        /// Parses the given string to the defined output type
        /// </summary>
        /// <param name="s">String that has to be converted</param>
        /// <returns>Hopefully the converted object as target type :)</returns>
        T Parse(string s);
    }
}