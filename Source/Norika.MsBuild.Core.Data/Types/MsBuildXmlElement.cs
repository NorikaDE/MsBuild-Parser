using System;
using System.Xml;
using Norika.MsBuild.Core.Data.Converter;
using Norika.MsBuild.Core.Data.Utilities;
using Norika.MsBuild.Model.Interfaces;
using Norika.Xml.CommentBasedHelp.Data;

namespace Norika.MsBuild.Core.Data.Types
{
    /// <summary>
    /// Provides basic implementation for an msbuild xml element
    /// </summary>
    public abstract class MsBuildXmlElement : IMsBuildElement
    {
        /// <summary>
        /// Name of the xml element representing this implementation
        /// </summary>
        public static string XmlElementName =>
            throw new InvalidOperationException($"Please overwrite {nameof(XmlElementName)} in derived classes.");

        protected readonly XmlElement XmlElement;

        public string Condition { get; }
        public bool IsConditional => !string.IsNullOrWhiteSpace(Condition);
        public IMsBuildElementHelp Help { get; }

        protected MsBuildXmlElement(XmlElement element)
        {
            XmlElement =
                element ?? throw new ArgumentNullException(nameof(element), "The xml element should not be null!");
            Condition = XmlElement.GetAttributeValue(nameof(Condition));
            Help = InitializeHelp();
        }

        /// <summary>
        /// Initializes the user defined help comment by crating a dictionary containing the help
        /// keywords and related content by using <seealso cref="XmlHelpKeyword"/>. 
        /// </summary>
        /// <returns>Dictionary containing the keywords and content of the user defined help comment</returns>
        private IMsBuildElementHelp InitializeHelp()
        {
            return XmlElement.GetHelp()?.ToMsBuildElementHelp();
        }
    }
}