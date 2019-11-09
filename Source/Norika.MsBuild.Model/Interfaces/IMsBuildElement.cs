namespace Norika.MsBuild.Model.Interfaces
{
    public interface IMsBuildElement
    {
        /// <summary>
        /// Raw condition string which is interpreted by msbuild
        /// </summary>
        string Condition { get; }

        /// <summary>
        /// Does the element have a condition implemented? 
        /// </summary>
        bool IsConditional { get; }

        /// <summary>
        /// User defined comment based help for the current msbuild element
        /// </summary>
        IMsBuildElementHelp Help { get; }
    }
}