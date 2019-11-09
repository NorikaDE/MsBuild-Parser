using System.Xml;
using Norika.MsBuild.Core.Data.Nodes;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Core.Data
{
    public static class MsBuildProjectFile
    {
        public static IMsBuildProject LoadContent(string content)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(content);
            return new MsBuildXmlProjectImplementation(document);
        }

        public static IMsBuildProject Load(string path)
        {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            return new MsBuildXmlProjectImplementation(document);
        }
    }
}