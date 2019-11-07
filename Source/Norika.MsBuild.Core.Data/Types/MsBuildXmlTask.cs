using System.Xml;
using Norika.MsBuild.Core.Data.Converter;
using Norika.MsBuild.Core.Data.Utilities;
using Norika.MsBuild.Model.Interfaces;
using Norika.MsBuild.Model.Types;

namespace Norika.MsBuild.Core.Data.Types
{
    /// <summary>
    /// Provides basic implementation for a msbuild task
    /// </summary>
    public abstract class MsBuildXmlTask : MsBuildXmlNode , IMsBuildTask
    {
        /// <inheritdoc cref="MsBuildXmlElement.XmlElementName"/>
        public new static string XmlElementName;
        
        protected MsBuildXmlTask(XmlElement element) : base(element)
        {
            MsBuildConverterFactory factory = new MsBuildConverterFactory();
            ContinueOnError = factory.CreateConverter<ContinueOnError>()
                .Parse(element.GetAttributeValue(nameof(ContinueOnError)));
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public ContinueOnError ContinueOnError { get; }
    }
}