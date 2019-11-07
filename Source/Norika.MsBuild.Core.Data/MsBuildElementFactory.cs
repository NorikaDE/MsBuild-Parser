using System;
using System.Xml;
using Norika.MsBuild.Core.Data.Nodes;
using Norika.MsBuild.Core.Data.Tasks;
using Norika.MsBuild.Model.Interfaces;
using Norika.MsBuild.Model.Interfaces.Tasks;

namespace Norika.MsBuild.Core.Data
{
    /// <summary>
    /// Factory for creating specific objects of the <seealso cref="IMsBuildElement"/>
    /// interface and all other derived interfaces.
    /// </summary>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class MsBuildElementFactory
    {
        /// <summary>
        /// Creates a new implemented object of the given interface type. Also checks if
        /// the given element is assignable to the given msbuild type. 
        /// </summary>
        /// <param name="element"><seealso cref="XmlElement"/> to create a msbuild element from</param>
        /// <typeparam name="T">Type to create a new object from</typeparam>
        /// <returns>Created object if the given element is assignable to the given type</returns>
        public virtual T Create<T>(XmlElement element) where T : IMsBuildElement
        {
            IMsBuildElement createdObject = null;
            
            if (typeof(T) == typeof(IMsBuildTarget) 
                && MsBuildXmlTargetImplementation.XmlElementName.Equals(element.Name, 
                    StringComparison.OrdinalIgnoreCase))
            {
                createdObject = new MsBuildXmlTargetImplementation(element);
            }

            if (typeof(T) == typeof(IMsBuildOnError)
                && MsBuildXmlOnErrorTaskImplementation.XmlElementName.Equals(element.Name, 
                    StringComparison.OrdinalIgnoreCase))
            {
                createdObject = new MsBuildXmlOnErrorTaskImplementation(element);
                
            }
            
            if (typeof(T) == typeof(IMsBuildPropertyGroup)
                && MsBuildXmlPropertyGroupImplementation.XmlElementName.Equals(element.Name, 
                    StringComparison.OrdinalIgnoreCase))
            {
                createdObject = new MsBuildXmlPropertyGroupImplementation(element);
                
            }
            return (T) createdObject;
        }
    }
}