using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Norika.MsBuild.Core.Data.Help
{
    public static class MsBuildElementHelpCodeBlockUtility
    {
        private static IDictionary<string, string> _escapingPattern = new Dictionary<string, string>()
        {
            {"<!~~", "<!--"},
            {"<#", "<!--"},
            {"#>", "-->"},
            {"~~>", "-->"}
        };


        public static MsBuildHelpElementCodeBlock Parse(string helpStringContent)
        {
            helpStringContent = Decode(helpStringContent);
            
            MsBuildHelpCodeBlockLanguage language = GetLanguage(helpStringContent);

            if (language == MsBuildHelpCodeBlockLanguage.Xml)
            {
                helpStringContent = FormatXml(helpStringContent);
            }
            
            return new MsBuildHelpElementCodeBlock(language, helpStringContent);
        }
        
        public static string Decode(string helpStringContent)
        {
            if (string.IsNullOrWhiteSpace(helpStringContent)) return helpStringContent;
            
            helpStringContent = HttpUtility.HtmlDecode(helpStringContent);

            foreach (var escapingPattern in _escapingPattern)
            {
                helpStringContent = helpStringContent.Replace(escapingPattern.Key, escapingPattern.Value);
            }

            return helpStringContent;
        }

        public static MsBuildHelpCodeBlockLanguage GetLanguage(string help)
        {
            try
            {
                var document = XDocument.Parse(help);
                if(string.IsNullOrWhiteSpace(document.ToString()) == false)
                    return MsBuildHelpCodeBlockLanguage.Xml;
            }
            catch (XmlException)
            {
                return MsBuildHelpCodeBlockLanguage.Sh;
            }

            return MsBuildHelpCodeBlockLanguage.Sh;
        }

        public static string FormatXml(string inputString)
        {
            StringBuilder xmlStringBuilder = new StringBuilder();

            XDocument xmlHelpDocument = new XDocument();
            
            XmlWriterSettings writerSettings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true,
                NewLineOnAttributes = true,
                ConformanceLevel = ConformanceLevel.Fragment
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(xmlStringBuilder, writerSettings))
            {
                foreach (XmlNode child in ParseXml(inputString).ChildNodes)
                {
                    child.WriteTo(xmlWriter);
                }
            }

            return xmlStringBuilder.ToString();
        }
        
        private static XmlDocument ParseXml(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            return document;
        }

        public static bool IsStringXml(string input)
        {
            string trimmedInputValue = input.Trim();
            
            if (string.IsNullOrWhiteSpace(trimmedInputValue) 
                    || !(trimmedInputValue.StartsWith("<") && trimmedInputValue.EndsWith(">")))
                return false;

            try
            {
                ParseXml(
                    string.Format(CultureInfo.InvariantCulture, "<root>{0}</root>", trimmedInputValue));
                return true;
            }
            catch (XmlException xmlException)
            {
                Debug.Write(xmlException);
                return false;
            }
        }
    }

    public struct MsBuildHelpElementCodeBlock
    {
        public MsBuildHelpElementCodeBlock(MsBuildHelpCodeBlockLanguage language, string content)
        {
            Language = language;
            Content = content;
        }
        
        public MsBuildHelpCodeBlockLanguage Language { get; }
        public string Content { get; }
    }
}