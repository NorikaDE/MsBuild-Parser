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
            return IsStringXml(help) ? MsBuildHelpCodeBlockLanguage.Xml : MsBuildHelpCodeBlockLanguage.Sh;
        }


        public static string FormatXml(string inputString)
        {
            StringBuilder xmlStringBuilder = new StringBuilder();

            XmlWriterSettings writerSettings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true,
                NewLineOnAttributes = true,
                ConformanceLevel = ConformanceLevel.Fragment,
                NewLineChars = "\n"
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(xmlStringBuilder, writerSettings))
            {
                foreach (XmlNode child in ParseXml(inputString).DocumentElement.ChildNodes)
                {
                    child.WriteTo(xmlWriter);
                }
            }

            return xmlStringBuilder.ToString();
        }

        private static XmlDocument ParseXml(string xml)
        {
            string rootedXml = $"<TempXmlRoot>{xml}</TempXmlRoot>";

            XmlDocument document = new XmlDocument();
            document.LoadXml(rootedXml);
            return document;
        }

        public static bool IsStringXml(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            string trimmedInputValue = input.Trim();

            if (!(trimmedInputValue.StartsWith("<") && trimmedInputValue.EndsWith(">")))
                return false;

            string decodedString = Decode(input);
            try
            {
                ParseXml(
                    string.Format(CultureInfo.InvariantCulture, "<root>{0}</root>", decodedString));
                return true;
            }
            catch (XmlException xmlException)
            {
                Debug.Write(xmlException);
                return false;
            }
        }
    }
}