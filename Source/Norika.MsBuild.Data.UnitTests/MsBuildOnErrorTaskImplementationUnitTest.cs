using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Tasks;
using Norika.MsBuild.Model.Types;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class MsBuildOnErrorTaskImplementationUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_WithNullElement_ShouldThrowArgumentNullException()
        {
            MsBuildXmlOnErrorTaskImplementation propertyGroupImplementation = 
                new MsBuildXmlOnErrorTaskImplementation(null);
        }
        
        [TestMethod]
        public void Construct_WithElementContainingOneTarget_ShouldInitializeTargets()
        {
            MsBuildXmlOnErrorTaskImplementation propertyGroupImplementation = 
                new MsBuildXmlOnErrorTaskImplementation(
                    CreateFromString("<OnError ExecuteTargets=\"TargetA\" />"));
            
            Assert.AreEqual("TargetA", propertyGroupImplementation.ExecuteTargets[0]);
        }
        
        [TestMethod]
        public void Construct_WithElementAndSetContinueOnErrorToErrorAndStop_ShouldInitializeContinueOnErrorToErrorAndStop()
        {
            MsBuildXmlOnErrorTaskImplementation propertyGroupImplementation = 
                new MsBuildXmlOnErrorTaskImplementation(
                    CreateFromString("<OnError ExecuteTargets=\"TargetA\" ContinueOnError=\"ErrorAndStop\" />"));
            
            Assert.AreEqual(ContinueOnError.ErrorAndStop, propertyGroupImplementation.ContinueOnError);
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