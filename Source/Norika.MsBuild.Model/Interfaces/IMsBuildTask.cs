using Norika.MsBuild.Model.Types;

namespace Norika.MsBuild.Model.Interfaces
{
    /// <summary>
    /// Representation of a msbuild task within a target
    /// </summary>
    public interface IMsBuildTask : IMsBuildNode
    {
        /// <summary>
        /// Defines if the process should be continued if the task fails
        /// </summary>
        ContinueOnError ContinueOnError { get; } 
    }
}