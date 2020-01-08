using System.Text;
using Norika.MsBuild.Core.Data.Help;
using Norika.MsBuild.Model.Interfaces;
using Norika.Xml.CommentBasedHelp.Data.Interfaces;

namespace Norika.MsBuild.Core.Data.Converter
{
    public class XmlHelpParagraphToMsBuildElementHelpParagraphConverter
    {
        public IMsBuildElementHelpParagraph Convert(IXmlCommentHelpParagraph xmlHelp)
        {
            StringBuilder stringContentBuilder = new StringBuilder();

            foreach (string line in xmlHelp.Content)
            {
                stringContentBuilder.Append(line.Trim());
                stringContentBuilder.Append('\n');
            }

            string content = stringContentBuilder.ToString();

            if (content.EndsWith('\n'))
                content = content.TrimEnd('\n');

            IMsBuildElementHelpParagraph msBuildElementHelp =
                new MsBuildElementHelpParagraph(xmlHelp.Name, content, xmlHelp.Additional);

            return msBuildElementHelp;
        }
    }
}