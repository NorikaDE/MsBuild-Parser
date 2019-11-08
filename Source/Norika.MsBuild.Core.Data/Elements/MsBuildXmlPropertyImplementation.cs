using System;
using System.Text.RegularExpressions;
using System.Xml;
using Norika.MsBuild.Core.Data.Types;
using Norika.MsBuild.Core.Data.Utilities;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Core.Data.Elements
{
    public class MsBuildXmlPropertyImplementation : MsBuildXmlElement, IMsBuildProperty
    {
        public static bool HasPropertyPublicSetter(string propertyName, string propertyCondition,
            string propertyContent)
        {
            try
            {
                if (string.IsNullOrEmpty(propertyCondition) &&
                    propertyContent.Contains(string.Format(MsBuildStringUtilities.FormatProvider, "{0:Property}",
                        propertyName)))
                    return true;

                if (string.IsNullOrEmpty(propertyCondition)) return false;

                RegexFactory factory = new RegexFactory();

                Regex selfIsEmptyCheck = factory.CreatePropertyConditionSelfCheckEmptyRegex(propertyName);

                if (selfIsEmptyCheck.IsMatch(propertyCondition))
                    return true;

                Regex selfIsNotEmptyCheck = factory.CreatePropertyConditionSelfCheckIsNotEmptyEmptyRegex(propertyName);

                return selfIsNotEmptyCheck.IsMatch(propertyCondition) &&
                       propertyContent.Contains(string.Format(MsBuildStringUtilities.FormatProvider, "{0:Property}",
                           propertyName));
            }
            catch (NullReferenceException nullReferenceException)
            {
                Console.WriteLine("Error while determining property {0}.", propertyName);
                return false;
            }
        }


        public MsBuildXmlPropertyImplementation(XmlElement element) : base(element)
        {
            Name = element.Name;

            Value = (string.IsNullOrWhiteSpace(element.InnerText) ? null : element.InnerText);
        }

        public string Name { get; }
        public string Value { get; }

        public bool HasPublicSetter => MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(Name, Condition, Value);
    }
}