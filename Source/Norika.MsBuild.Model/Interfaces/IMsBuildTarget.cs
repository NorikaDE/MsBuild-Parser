using System.Collections.Generic;

namespace Norika.MsBuild.Model.Interfaces
{
    /// <summary>
    /// A MsBuild target
    /// </summary>
    public interface IMsBuildTarget : IMsBuildNode
    {
        /// <summary>
        /// Name of the target used by msbuild for invoking the target
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Depending target names. Targets in that list have to be executed before the current target.
        /// </summary>
        IList<string> DependsOnTargets { get; }

        /// <summary>
        /// Indicates for which targets the current target have to be executed before. 
        /// </summary>
        IList<string> BeforeTargets { get; }

        /// <summary>
        /// Indicates for which targets the current targets have to be executed afterwards. 
        /// </summary>
        IList<string> AfterTargets { get; }

        /// <summary>
        /// Indicates which targets are run if the current target fails.
        /// </summary>
        IList<string> OnErrorTargets { get; }

        /// <summary>
        /// Indicates if the target has some dependencies to other targets.
        /// </summary>
        bool HasTargetDependencies { get; }
    }
}