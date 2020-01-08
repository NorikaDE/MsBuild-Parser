using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Nodes;
using Norika.MsBuild.Model.Interfaces;
using Norika.MsBuild.Model.Interfaces.Tasks;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    [DeploymentItem("TestData/DefaultTarget.xml")]
    public class MsBuildTargetImplementationUnitTest
    {
        private readonly XmlDocument _document = new XmlDocument();
        private XmlElement _defaultTargetElement;
        private XmlElement _extendedTargetElement;

        [TestInitialize]
        public void InitializeTest()
        {
            _document.Load("TestData/DefaultTarget.xml");
            _defaultTargetElement = (XmlElement) _document.GetElementsByTagName("Target")[0];
            _extendedTargetElement = (XmlElement) _document.GetElementsByTagName("Target")[1];
        }

        [TestMethod]
        public void TestMethod1()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_defaultTargetElement);

            Assert.AreEqual("DefaultTarget", target.Name);
        }

        [TestMethod]
        public void TestMethod2()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_defaultTargetElement);

            Assert.AreEqual("DependentTarget", target.DependsOnTargets.First());
        }

        [TestMethod]
        public void TestMethod3()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_defaultTargetElement);

            Assert.AreEqual("BeforeTargets", target.BeforeTargets.First());
        }

        [TestMethod]
        public void TestMethod4()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_defaultTargetElement);

            Assert.AreEqual("AfterTargets", target.AfterTargets.First());
        }

        [TestMethod]
        public void TestMethod5()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_defaultTargetElement);

            Assert.IsTrue(target.IsConditional);
            Assert.AreEqual("'String A' == 'String B'", target.Condition);
        }

        [TestMethod]
        public void TestMethod6()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_defaultTargetElement);

            Assert.AreEqual("OnErrorTargetName", target.OnErrorTargets.First());
        }

        [TestMethod]
        public void TestMethod7()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_defaultTargetElement);

            Assert.AreEqual("This is a test target", target.Help.LookUp("SYNOPSIS").First().Content);
            Assert.AreEqual("Does nothing really cool but, yeah, well...",
                target.Help.LookUp("DESCRIPTION").First().Content);
        }

        [TestMethod]
        public void TestMethod11()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_extendedTargetElement);

            Assert.AreEqual("ExtendedTarget", target.Name);
        }

        [TestMethod]
        public void TestMethod12()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_extendedTargetElement);

            Assert.AreEqual("BeforeTargets1", target.BeforeTargets[0]);
            Assert.AreEqual("BeforeTargets2", target.BeforeTargets[1]);
        }

        [TestMethod]
        public void TestMethod13()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_extendedTargetElement);

            Assert.AreEqual("AfterTargets1", target.AfterTargets[0]);
            Assert.AreEqual("AfterTargets2", target.AfterTargets[1]);
            Assert.AreEqual("AfterTargets3", target.AfterTargets[2]);
        }

        [TestMethod]
        public void TestMethod14()
        {
            IMsBuildTarget target = new MsBuildXmlTargetImplementation(_defaultTargetElement);

            Assert.AreEqual("This is a test target", target.Help.LookUp("SYNOPSIS").First().Content);
            Assert.AreEqual("Does nothing really cool but, yeah, well...",
                target.Help.LookUp("DESCRIPTION").First().Content);
        }

        [TestMethod]
        public void GetChildren_FromTargetWithCommentInBody_ShouldNotThrowException()
        {
            string testTargetImplementation =
                "<Project><Target Name=\"Test\"><!-- This is a comment --><CallTarget Targets=\"AnotherTest\" /></Target></Project>";

            XmlDocument document = new XmlDocument();
            document.LoadXml(testTargetImplementation);
            XmlElement targetElement = (XmlElement) document.GetElementsByTagName("Target")[0];

            IMsBuildTarget target = new MsBuildXmlTargetImplementation(targetElement);

            target.GetChildren<IMsBuildOnError>();
        }

        [TestMethod]
        public void HasTargetDependencies_FromTargetWithDependsOnTarget_ShouldReturnTrue()
        {
            string testTargetImplementation =
                "<Project><Target Name=\"Test\" DependsOnTargets=\"OtherTest\"><CallTarget Targets=\"AnotherTest\" /></Target></Project>";

            XmlDocument document = new XmlDocument();
            document.LoadXml(testTargetImplementation);
            XmlElement targetElement = (XmlElement) document.GetElementsByTagName("Target")[0];

            IMsBuildTarget target = new MsBuildXmlTargetImplementation(targetElement);

            Assert.IsTrue(target.HasTargetDependencies);
        }

        [TestMethod]
        public void HasTargetDependencies_FromTargetWithoutDependsOnTarget_ShouldReturnTrue()
        {
            string testTargetImplementation =
                "<Project><Target Name=\"Test\"><CallTarget Targets=\"AnotherTest\" /></Target></Project>";

            XmlDocument document = new XmlDocument();
            document.LoadXml(testTargetImplementation);
            XmlElement targetElement = (XmlElement) document.GetElementsByTagName("Target")[0];

            IMsBuildTarget target = new MsBuildXmlTargetImplementation(targetElement);

            Assert.IsFalse(target.HasTargetDependencies);
        }
    }
}