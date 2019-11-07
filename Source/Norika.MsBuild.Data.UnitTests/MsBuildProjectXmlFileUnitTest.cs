using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data;
using Norika.MsBuild.Model.Interfaces;
using Norika.MsBuild.Model.Interfaces.Tasks;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    [DeploymentItem("TestData/TestProject.xml")]
    public class MsBuildProjectXmlFileUnitTest
    {
        [TestMethod]
        public void Load_FromValidInputXml_ShouldInitializeMsBuildToolsVersion()
        {
            string inputValue = File.ReadAllText("TestData/TestProject.xml");

            IMsBuildProject project = MsBuildProjectFile.LoadContent(inputValue);
            
            Assert.AreEqual("15.0", project.MsBuildVersion);
        }
        
        [TestMethod]
        public void Load_FromValidInputXml_ShouldInitializeDefaultTargets()
        {
            string inputValue = File.ReadAllText("TestData/TestProject.xml");

            IMsBuildProject project = MsBuildProjectFile.LoadContent(inputValue);
            
            Assert.AreEqual("TestTargetA", project.DefaultTargets[0]);
        }
        
        [TestMethod]
        public void Load_FromValidInputXml_ShouldInitializeInitializeTargets()
        {
            string inputValue = File.ReadAllText("TestData/TestProject.xml");

            IMsBuildProject project = MsBuildProjectFile.LoadContent(inputValue);
            
            Assert.AreEqual("TestTargetB", project.InitialTargets[0]);
        }
        
        [TestMethod]
        public void GetChildItem_PropertyGroupFromValidInputXml_ShouldInitializeAllThreeProperties()
        {
            string inputValue = File.ReadAllText("TestData/TestProject.xml");

            IMsBuildProject project = MsBuildProjectFile.LoadContent(inputValue);
            
            Assert.AreEqual(3, project.GetChildren<IMsBuildPropertyGroup>()
                .SelectMany(x => x.GetChildren<IMsBuildProperty>()).Count());
        }
        
        [TestMethod]
        public void GetChildItem_PropertyGroupFromValidInputXmlAndNotExistentChild_ShouldReturnEmptyList()
        {
            string inputValue = File.ReadAllText("TestData/TestProject.xml");

            IMsBuildProject project = MsBuildProjectFile.LoadContent(inputValue);
            
            Assert.AreEqual(0, project.GetChildren<IMsBuildPropertyGroup>()
                .SelectMany(x => x.GetChildren<IMsBuildOnError>()).Count());
        }
        
        [TestMethod]
        public void GetChildItem_TargetsFromValidInputXml_ShouldInitializeAllTwoTargets()
        {
            string inputValue = File.ReadAllText("TestData/TestProject.xml");

            IMsBuildProject project = MsBuildProjectFile.LoadContent(inputValue);
            
            Assert.AreEqual(2, project.GetChildren<IMsBuildTarget>().Count);
        }
    }
}