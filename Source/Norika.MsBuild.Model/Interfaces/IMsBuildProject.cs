using System.Collections.Generic;

namespace Norika.MsBuild.Model.Interfaces
{
    public interface IMsBuildProject : IList<IMsBuildElement>, IMsBuildNode
    {
        string MsBuildVersion { get; }
        
        IList<string> InitialTargets { get; }
        IList<string> DefaultTargets { get; }
    }
}