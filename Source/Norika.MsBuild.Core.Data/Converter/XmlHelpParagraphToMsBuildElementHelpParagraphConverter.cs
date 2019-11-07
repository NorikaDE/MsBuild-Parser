using System;
using System.Text;
using Norika.MsBuild.Core.Data.Help;
using Norika.MsBuild.Model.Interfaces;
using Norika.Xml.CommentBasedHelp.Data;
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
                stringContentBuilder.Append(Environment.NewLine);
            }

            string content = stringContentBuilder.ToString();

            if (content.EndsWith(Environment.NewLine))
                content = content.TrimEnd(Environment.NewLine.ToCharArray());
            
            IMsBuildElementHelpParagraph msBuildElementHelp = new MsBuildElementHelpParagraph(xmlHelp.Name, content, xmlHelp.Additional);
            
            return msBuildElementHelp;
        }

       
    }
}