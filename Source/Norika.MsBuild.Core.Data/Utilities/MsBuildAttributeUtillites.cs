using System.Collections.Generic;
using System.Xml;

namespace Norika.MsBuild.Core.Data.Utilities
{
    /// <summary>
    /// Provides methods for getting further information from xml elements
    /// </summary>
    public static class MsBuildAttributeUtilities
    {
        /// <summary>
        /// Initializes the content of the given attribute name for the current target. 
        /// </summary>
        /// <param name="element">Extended object</param>
        /// <param name="attributeName">Attribute name which have to be initialized.</param>
        /// <returns>Attribute content of the given attribute name.</returns>
        public static string GetAttributeValue(this XmlElement element, string attributeName)
        {
            string name = element.GetAttribute(attributeName);
            return string.IsNullOrEmpty(name) ? null : name;
        }

        /// <summary>
        /// Initializes the given attribute content list for the current target. If the content
        /// has to be split the <seealso cref="MsBuildStringUtilities"/>-class is used. 
        /// </summary>
        /// <param name="element">Extended object</param>
        /// <param name="attributeName">Name of the attribute</param>
        /// <returns>List of strings representing the attribute content</returns>
        public static IList<string> GetAttributeValueList(this XmlElement element, string attributeName)
        {
            string attributeContent = element.GetAttribute(attributeName);
            return string.IsNullOrEmpty(attributeContent)
                ? new List<string>()
                : attributeContent.SplitByDefaultSeparator();
        }
    }
}