using System.Collections.Generic;
using System.Xml;
using Norika.MsBuild.Core.Data.Types;
using Norika.MsBuild.Core.Data.Utilities;
using Norika.MsBuild.Model.Interfaces.Tasks;

namespace Norika.MsBuild.Core.Data.Tasks
{
    public class MsBuildXmlOnErrorTaskImplementation : MsBuildXmlTask, IMsBuildOnError
    {
        /// <inheritdoc cref="IMsBuildOnError.ExecuteTargets"/>
        public IList<string> ExecuteTargets { get; }

        public MsBuildXmlOnErrorTaskImplementation(XmlElement element) : base(element)
        {
            ExecuteTargets = element.GetAttributeValueList(nameof(ExecuteTargets));
        }

        public new static string XmlElementName => "OnError";
    }
}