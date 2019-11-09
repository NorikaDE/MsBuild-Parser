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
        /// <summary>
        /// Determines if a a property with given specification is public settable or not
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyCondition"></param>
        /// <param name="propertyContent"></param>
        /// <returns></returns>
        public static bool HasPropertyPublicSetter(string propertyName, string propertyCondition,
            string propertyContent)
        {
            if (string.IsNullOrWhiteSpace(propertyContent) && string.IsNullOrEmpty(propertyCondition))
                return false;

            // Todo: Fix possible null reference 
            if (string.IsNullOrEmpty(propertyCondition) &&
                propertyContent.Contains(string.Format(MsBuildStringUtilities.FormatProvider, "{0:Property}",
                    propertyName)))
                return true;

            if (string.IsNullOrEmpty(propertyCondition)) return false;

            RegexFactory factory = new RegexFactory();

            Regex selfIsEmptyCheck = factory.CreatePropertyConditionSelfCheckEmptyRegex(propertyName);

            if (selfIsEmptyCheck.IsMatch(propertyCondition)) return true;

            Regex selfIsNotEmptyCheck = factory.CreatePropertyConditionSelfCheckIsNotEmptyEmptyRegex(propertyName);

            return selfIsNotEmptyCheck.IsMatch(propertyCondition) && !string.IsNullOrWhiteSpace(propertyContent) &&
                   propertyContent.Contains(string.Format(MsBuildStringUtilities.FormatProvider, "{0:Property}",
                       propertyName));
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