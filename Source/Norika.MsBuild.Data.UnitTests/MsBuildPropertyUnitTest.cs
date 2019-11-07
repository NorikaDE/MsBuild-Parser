using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Elements;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    // ToDo: Implementing following overwrite check: <Property Condition="$(OtherProperty) != '')>$(OtherProperty)</Property>
    public class MsBuildPropertyUnitTest
    {

        [TestMethod]
        public void IsPropertyPublicSettable_WithCheckForPropertyIsEmptyInCondition_ShouldReturnTrue()
        {
            string propertyName = "TestProperty";
            string propertyCondition = "$(TestProperty) == ''";
            string propertyContent = string.Empty;

            Assert.IsTrue(MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition, propertyContent));
        }

        [TestMethod]
        public void IsPropertyPublicSettable_WithoutConditionAndContent_ShouldReturnFalse()
        {
            string propertyName = "TestProperty";
            string propertyCondition = string.Empty;
            string propertyContent = string.Empty;

            Assert.IsFalse(MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition, propertyContent));
        }

        [TestMethod]
        public void IsPropertyPublicSettable_WithConditionCheckingSomeOtherPropertiesAndNoPropertyValueTakeOverInContent_ShouldReturnFalse()
        {
            string propertyName = "TestProperty";
            string propertyCondition = "$(OtherProperty) == 'value'";
            string propertyContent = string.Empty;

            Assert.IsFalse(MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition, propertyContent));
        }


        [TestMethod]
        public void IsPropertyPublicSettable_WithConditionCheckingSomeOtherPropertiesAndPropertyValueTakeOverInContent_ShouldReturnFalse()
        {
            string propertyName = "TestProperty";
            string propertyCondition = "$(OtherProperty) == 'value'";
            string propertyContent = "$(TestProperty)";

            Assert.IsFalse(MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition, propertyContent));
        }

        [TestMethod]
        public void IsPropertyPublicSettable_WithComplexConditionCheckingPropertyIsNotEmptyAndPropertyValueTakeOverInContent_ShouldReturnTrue()
        {
            string propertyName = "TestProperty";
            string propertyCondition = "$(OtherProperty) == 'value' AND $(TestProperty) != ''";
            string propertyContent = "$(TestProperty)";

            Assert.IsTrue(MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition, propertyContent));
        }

        [TestMethod]
        public void IsPropertyPublicSettable_WithCheckForPropertyIsNotEmptyInConditionAndSetPropertyInContent_ShouldReturnTrue()
        {
            string propertyName = "TestProperty";
            string propertyCondition = "$(TestProperty) != ''";
            string propertyContent = "$(TestProperty)";

            Assert.IsTrue(MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition, propertyContent));
        }


        [TestMethod]
        public void IsPropertyPublicSettable_WithCheckForPropertyIsNotEmptyInConditionAndTakeOverPropertyValueInAdditionalContent_ShouldReturnTrue()
        {
            string propertyName = "TestProperty";
            string propertyCondition = "$(TestProperty) != ''";
            string propertyContent = "SomeOtherValues $(TestProperty)";

            Assert.IsTrue(MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition, propertyContent));
        }

        [TestMethod]
        public void IsPropertyPublicSettable_WithCheckForPropertyIsNotEmptyInConditionAndNoPropertyValueTakeOverInContent_ShouldReturnFalse()
        {
            string propertyName = "TestProperty";
            string propertyCondition = "$(TestProperty) != ''";
            string propertyContent = "SomeOtherValues $(OtherProperty)";

            Assert.IsFalse(MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition, propertyContent));
        }
        
        [TestMethod]
        public void Construct_PropertyWithName_ShouldInitializeProperty()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(CreateFromString("<Property></Property>"));
            
            Assert.AreEqual("Property", property.Name);
        }
        
        [TestMethod]
        public void Construct_PropertyWithNameAndValue_ShouldInitializePropertyAndValue()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(CreateFromString("<Property>Value</Property>"));
            
            Assert.AreEqual("Property", property.Name);
            Assert.AreEqual("Value", property.Value);
        }
        
        [TestMethod]
        public void Construct_PropertyWithoutValue_ShouldInitializePropertyWithNullValue()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(CreateFromString("<Property></Property>"));
            
            Assert.IsNull(property.Value);
        }
        
        [TestMethod]
        public void Construct_PropertyWithOverwriteAsSelfNullCheck_ShouldInitializeWithIsSettableTrue()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(CreateFromString("<Property Condition=\"$(Property)==''\">Value</Property>"));
            
            Assert.IsTrue(property.HasPublicSetter);
        }
        
        [TestMethod]
        public void Construct_PropertyWithTakeOverValue_ShouldInitializeWithIsSettableTrue()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(CreateFromString("<Property Condition=\"$(Property)!=''\">$(Property) Value</Property>"));
            
            Assert.IsTrue(property.HasPublicSetter);
        }
        
        [TestMethod]
        public void Construct_PropertyWithoutTakeOverValue_ShouldInitializeWithIsSettableFalse()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(CreateFromString("<Property Condition=\"$(Property)!=''\">$(NotProperty) Value</Property>"));
            
            Assert.IsFalse(property.HasPublicSetter);
        }
        
        [TestMethod]
        public void Construct_PropertyWithoutOverwriteAsSelfNullCheck_ShouldInitializeWithIsSettableFalse()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(CreateFromString("<Property>Value</Property>"));
            
            Assert.IsFalse(property.HasPublicSetter);
        }
        
        [TestMethod]
        public void Construct_PropertyWithoutConditionAndValueIsProperty_ShouldInitializeWithIsSettableTrue()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(CreateFromString("<Property>$(Property)</Property>"));
            
            Assert.IsTrue(property.HasPublicSetter);
        }
        
        [TestMethod]
        public void Construct_PropertyWithoutConditionAndValueIsDifferentProperty_ShouldInitializeWithIsSettableFalse()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(CreateFromString("<Property>$(NotProperty)</Property>"));
            
            Assert.IsFalse(property.HasPublicSetter);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_WithNullInput_ShouldThrowException()
        {
            MsBuildXmlPropertyImplementation property = new MsBuildXmlPropertyImplementation(null);
        }
        
        private XmlElement CreateFromString(string elementString)
        {
            XmlDocument document = new XmlDocument();
            
            document.LoadXml(elementString);

            XmlElement propertyValueElement = (XmlElement) document.ChildNodes[0];

            return propertyValueElement;
        }


    }
}
