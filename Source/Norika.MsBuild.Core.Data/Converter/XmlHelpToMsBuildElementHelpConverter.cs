using Norika.MsBuild.Core.Data.Help;
using Norika.MsBuild.Model.Interfaces;
using Norika.Xml.CommentBasedHelp.Data.Interfaces;

namespace Norika.MsBuild.Core.Data.Converter
{
    public class XmlHelpToMsBuildElementHelpConverter
    {
        public IMsBuildElementHelp Convert(IXmlHelp xmlHelp)
        {
            IMsBuildElementHelp msBuildElementHelp = new MsBuildElementHelp();
            XmlHelpParagraphToMsBuildElementHelpParagraphConverter converter = 
                new XmlHelpParagraphToMsBuildElementHelpParagraphConverter();
            
            foreach (var xmlHelpParagraph in xmlHelp)
            {
                msBuildElementHelp.Add(converter.Convert(xmlHelpParagraph));
            }
            return msBuildElementHelp;
        }

       
    }
}