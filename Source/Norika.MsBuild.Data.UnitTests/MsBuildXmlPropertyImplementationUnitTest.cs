using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Elements;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class MsBuildXmlPropertyImplementationUnitTest
    {
        [TestMethod]
        public void HasPropertyPublicSetter_WithEmptyConditionAndPropertyNameAsContent_ReturnTrue()
        {
            string propertyContent = "$(Test)";
            string propertyCondition = null;
            string propertyName = "Test";

            Assert.IsTrue(
                MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition,
                    propertyContent),
                "Property without condition and self set in content should return true");
        }

        [TestMethod]
        public void HasPropertyPublicSetter_WithEmptyConditionAndPropertyNameAsContentWithAdditionalContent_ReturnTrue()
        {
            string propertyContent = "$(Test)\\test";
            string propertyCondition = null;
            string propertyName = "Test";

            Assert.IsTrue(
                MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition,
                    propertyContent),
                "Property without condition and self set in content should return true");
        }

        [TestMethod]
        public void HasPropertyPublicSetter_WithNullContentAndNullCondition_ReturnTrue()
        {
            string propertyContent = null;
            string propertyCondition = null;
            string propertyName = "Test";

            Assert.IsFalse(
                MsBuildXmlPropertyImplementation.HasPropertyPublicSetter(propertyName, propertyCondition,
                    propertyContent),
                "Property without content and condition should return false");
        }
    }
}