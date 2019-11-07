using System.Collections.Generic;
using System.Xml;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Core.Data.Types
{
    /// <summary>
    /// Provides basic implementation for a msbuild xml node
    /// </summary>
    public abstract class MsBuildXmlNode : MsBuildXmlElement, IMsBuildNode
    {
        /// <inheritdoc cref="MsBuildXmlElement.XmlElementName"/>
        public new static string XmlElementName;
        

        /// <inheritdoc /> 
        public virtual IList<T> GetChildren<T>() where T : class, IMsBuildElement
        {
            IList<T> foundMatchingChildItems = new List<T>();
            MsBuildElementFactory factory = new MsBuildElementFactory();

            foreach (XmlElement childElement in XmlElement.ChildNodes)
            {
                T createdObject = factory.Create<T>(childElement);
                if(createdObject != null)
                    foundMatchingChildItems.Add(createdObject);
            }
            return foundMatchingChildItems;
        }

        protected MsBuildXmlNode(XmlElement element) : base(element) { }
        
        
    }
}