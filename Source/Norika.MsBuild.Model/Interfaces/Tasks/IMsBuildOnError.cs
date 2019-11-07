using System.Collections.Generic;

namespace Norika.MsBuild.Model.Interfaces.Tasks
{
    /// <summary>
    /// Representing the msbuild 'OnError' task. Defines targets that should be executed
    /// if the current target fails during execution.
    /// </summary>
    public interface IMsBuildOnError : IMsBuildTask
    {
        /// <summary>
        /// Targets that should be executed after the current target if it fails. 
        /// </summary>
        IList<string> ExecuteTargets { get; }
    }
}