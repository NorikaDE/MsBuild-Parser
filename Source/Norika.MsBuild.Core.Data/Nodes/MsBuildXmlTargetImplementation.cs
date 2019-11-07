using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Norika.MsBuild.Core.Data.Types;
using Norika.MsBuild.Core.Data.Utilities;
using Norika.MsBuild.Model.Interfaces;
using Norika.MsBuild.Model.Interfaces.Tasks;

namespace Norika.MsBuild.Core.Data.Nodes
{
    /// <summary>
    /// Implementation of the Target-Interface for default msbuild xml format
    /// </summary>
    public class MsBuildXmlTargetImplementation : MsBuildXmlNode, IMsBuildTarget
    {
        public new static string XmlElementName = "Target";
        
        public MsBuildXmlTargetImplementation(XmlElement targetNode) : base(targetNode)
        {
            Name = targetNode.GetAttributeValue(nameof(Name));
           
            DependsOnTargets = targetNode.GetAttributeValueList(nameof(DependsOnTargets));
            BeforeTargets = targetNode.GetAttributeValueList(nameof(BeforeTargets));
            AfterTargets = targetNode.GetAttributeValueList(nameof(AfterTargets));
            OnErrorTargets = InitializeOnErrorTargets();
        }

       

        /// <summary>
        /// Initializes the targets OnError-Setting by returning the name of the targets which
        /// are executed on error in the current target.
        /// </summary>
        /// <returns>List of target names that are executed on error in the current target</returns>
        private IList<string> InitializeOnErrorTargets()
        {
             IMsBuildOnError onErrorObject = GetChildren<IMsBuildOnError>().LastOrDefault();
             if (onErrorObject == null)
             {
                 return new List<string>();
             }
             else
             {
                 return onErrorObject.ExecuteTargets;
             }
        }
        
        /// <summary>
        /// <inheritdoc cref="IMsBuildTarget.Name"/>
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// <inheritdoc cref="IMsBuildTarget.DependsOnTargets"/>
        /// </summary>
        public IList<string> DependsOnTargets { get; }
        
        /// <summary>
        /// <inheritdoc cref="IMsBuildTarget.BeforeTargets"/>
        /// </summary>
        public IList<string> BeforeTargets { get; }
        
        /// <summary>
        /// <inheritdoc cref="IMsBuildTarget.AfterTargets"/>
        /// </summary>
        public IList<string> AfterTargets { get; }
        
        /// <summary>
        /// <inheritdoc cref="IMsBuildTarget.OnErrorTargets"/>
        /// </summary>
        public IList<string> OnErrorTargets { get; }

        /// <summary>
        /// <inheritdoc cref="IMsBuildTarget.HasTargetDependencies"/>
        /// </summary>
        public bool HasTargetDependencies =>
            (AfterTargets.Count > 0 || BeforeTargets.Count > 0 || DependsOnTargets.Count > 0);
    }
}