using System.Collections.Generic;

namespace Norika.MsBuild.Model.Interfaces
{
    public interface IMsBuildNode : IMsBuildElement
    {
        /// <summary>
        /// Return the children elements of the current implementation
        /// matching the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetChildren<T>() where T : class, IMsBuildElement;
    }
}