using Norika.MsBuild.Model.Interfaces;
using Norika.Xml.CommentBasedHelp.Data.Interfaces;

namespace Norika.MsBuild.Core.Data.Converter
{
    public static class XmlHelpExtension
    {
        public static IMsBuildElementHelp ToMsBuildElementHelp(this IXmlHelp xmlHelp)
        {
            XmlHelpToMsBuildElementHelpConverter converter = new XmlHelpToMsBuildElementHelpConverter();
            return converter.Convert(xmlHelp);
        }
        
        public static IMsBuildElementHelpParagraph ToMsBuildElementHelp(this IXmlCommentHelpParagraph xmlHelp)
        {
            XmlHelpParagraphToMsBuildElementHelpParagraphConverter converter = new XmlHelpParagraphToMsBuildElementHelpParagraphConverter();
            return converter.Convert(xmlHelp);
        }
    }
}