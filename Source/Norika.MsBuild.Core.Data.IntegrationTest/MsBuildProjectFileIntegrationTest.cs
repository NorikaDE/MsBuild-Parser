using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Core.Data.IntegrationTest
{
    [TestClass]
    public class MsBuildProjectFileIntegrationTest
    {
        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/Test.tcsproj")]
        public void Load_FromExistentTestFile_ShouldNotReturnNullObject()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/Test.tcsproj");

            Assert.IsNotNull(projectFile);
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/Test.tcsproj")]
        public void Load_FromExistentTestFileWithTwoTargets_ShouldReturnObjectWithTwoContainingTargets()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/Test.tcsproj");

            Assert.AreEqual(2, projectFile.GetChildren<IMsBuildTarget>().Count);
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/Test.tcsproj")]
        public void
            Load_FromExistentTestFileWithOnePropertyGroupContainingThreeProperties_ShouldReturnObjectContainingThreeProperties()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/Test.tcsproj");

            Assert.AreEqual(3, projectFile.GetChildren<IMsBuildProperty>().Count);
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/Test.tcsproj")]
        public void
            Load_FromExistentTestFileWithTwoTargets_ShouldReturnObjectWithTwoContainingTargetsWithCorrectInitializedName()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/Test.tcsproj");

            IList<IMsBuildTarget> targets = projectFile.GetChildren<IMsBuildTarget>();

            Assert.IsTrue(targets.Any(t => t.Name.Equals("CheckFileNameSyntax")));
            Assert.IsTrue(targets.Any(t => t.Name.Equals("BeforeCompile")));
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/Test.tcsproj")]
        public void Load_FromExistentTestFileWithTwoTargets_ShouldReturnObjectContainingNoNotExistentTarget()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/Test.tcsproj");

            IList<IMsBuildTarget> targets = projectFile.GetChildren<IMsBuildTarget>();

            Assert.IsFalse(targets.Any(t => t.Name.Equals("NotExistentTarget")));
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/Test.tcsproj")]
        public void Load_FromExistentTestFileWithPropertyNamedAuthor_ShouldReturnObjectContainingAnPropertyNamedAuthor()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/Test.tcsproj");

            IList<IMsBuildProperty> properties = projectFile.GetChildren<IMsBuildProperty>();

            Assert.IsTrue(properties.Any(p => p.Name.Equals("Author")));
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/Test.tcsproj")]
        public void
            Load_FromExistentTestFileWithPropertyNamedAuthor_ShouldReturnObjectContainingAnPropertyNamedAuthorWithCorrectValue()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/Test.tcsproj");

            IList<IMsBuildProperty> properties = projectFile.GetChildren<IMsBuildProperty>();

            Assert.IsTrue(properties.Any(p => p.Name.Equals("Author")));
            Assert.AreEqual("Viktor-Fabian Müller", properties.FirstOrDefault(p => p.Name.Equals("Author"))?.Value);
        }


        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/Test.tcsproj")]
        public void
            Load_FromExistentTestFileWithTargetExecutedBeforeOtherTarget_ShouldReturnObjectContainingTargetWithCorrectBeforeTargetValue()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/Test.tcsproj");

            IMsBuildTarget target =
                projectFile.GetChildren<IMsBuildTarget>().First(t => t.Name.Equals("BeforeCompile"));

            Assert.AreEqual("CoreCompile", target.BeforeTargets.First());
        }


        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/Test.tcsproj")]
        public void
            Load_FromExistentTestFileWithDocumentedTargetNamedCheckFileNameSyntax_ShouldContainTargetWithXmlHelp()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/Test.tcsproj");

            IMsBuildTarget target = projectFile.GetChildren<IMsBuildTarget>()
                .First(t => t.Name.Equals("CheckFileNameSyntax"));

            Assert.AreEqual("Checks the files from @(RelevantFiles) if it ends with '*.json'.",
                target.Help.GetSectionContent("Synopsis", StringComparison.OrdinalIgnoreCase));
            Assert.AreEqual(
                "Prints an error with information about the item when it does not\nmatches the file extension 'json'. The printed error contains\ninformation about the affected item.",
                target.Help.GetSectionContent("DESCRIPTION", StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/TargetWithOnError.tcsproj")]
        public void
            Load_FromExistentTestFileWithOneTargetContainingOnErrorImplementation_ShouldContainTargetWithOnErrorImplementation()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/TargetWithOnError.tcsproj");

            IMsBuildTarget target =
                projectFile.GetChildren<IMsBuildTarget>().First(t => t.Name.Equals("BeforeCompile"));

            Assert.IsNotNull(target.OnErrorTargets);
            Assert.AreNotEqual(0, target.OnErrorTargets.Count);
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/TargetWithCommentInside.tcsproj")]
        public void Load_FromExistentTestFileWithTargetContainingComment_ShouldInitializeTargetsOnErrorImplementation()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/TargetWithOnError.tcsproj");

            IMsBuildTarget target =
                projectFile.GetChildren<IMsBuildTarget>().First(t => t.Name.Equals("BeforeCompile"));

            Assert.IsNotNull(target.OnErrorTargets);
            Assert.AreNotEqual(0, target.OnErrorTargets.Count);
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/TargetWithOnError.tcsproj")]
        public void
            Load_FromExistentTestFileWithOneTargetContainingOnErrorImplementation_ShouldContainTargetWithCorrectNamedOnErrorImplementation()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/TargetWithOnError.tcsproj");

            IMsBuildTarget target =
                projectFile.GetChildren<IMsBuildTarget>().First(t => t.Name.Equals("BeforeCompile"));

            Assert.AreEqual(1, target.OnErrorTargets.Count);
            Assert.AreEqual("CheckFileNameSyntax", target.OnErrorTargets.First());
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/TargetWithProperties.tcsproj")]
        public void
            Load_FromExistentTestFileWithTargetImplementingSimpleOverwritableProperty_ShouldReturnProjectContainingTargetWithOverwritableProperty()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/TargetWithProperties.tcsproj");

            IMsBuildTarget target = projectFile.GetChildren<IMsBuildTarget>()
                .First(t => t.Name.Equals("WithDefaultOverwritableProperty"));

            IMsBuildProperty overwritableProperty =
                target.GetChildren<IMsBuildPropertyGroup>().First().GetChildren<IMsBuildProperty>()
                    .First(p => p.Name.Equals("OverwritableProperty"));

            Assert.AreEqual(true, overwritableProperty.HasPublicSetter);
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/TargetWithProperties.tcsproj")]
        public void
            Load_FromExistentTestFileWithTargetImplementingNotOverwritableProperty_ShouldReturnProjectContainingTargetWithNotOverwritableProperty()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/TargetWithProperties.tcsproj");

            IMsBuildTarget target = projectFile.GetChildren<IMsBuildTarget>()
                .First(t => t.Name.Equals("WithNotOverwritableProperty"));

            IMsBuildProperty notOverwritableProperty =
                target.GetChildren<IMsBuildPropertyGroup>().First().GetChildren<IMsBuildProperty>()
                    .First(p => p.Name.Equals("Property"));

            Assert.AreEqual(false, notOverwritableProperty.HasPublicSetter);
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/TargetWithProperties.tcsproj")]
        public void
            Load_FromExistentTestFileWithTargetImplementingConditionalOverwritableProperty_ShouldReturnProjectContainingTargetWithOverwritableProperty()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/TargetWithProperties.tcsproj");

            IMsBuildTarget target = projectFile.GetChildren<IMsBuildTarget>()
                .First(t => t.Name.Equals("WithConditionalOverwritableProperty"));

            IMsBuildProperty overwritableProperty =
                target.GetChildren<IMsBuildPropertyGroup>().First().GetChildren<IMsBuildProperty>()
                    .First(p => p.Name.Equals("OverwritableProperty"));

            Assert.AreEqual(true, overwritableProperty.HasPublicSetter);
        }

        [TestMethod]
        [DeploymentItem("TestData/ProjectFiles/TargetWithProperties.tcsproj")]
        public void
            Load_FromExistentTestFileWithTargetImplementingComplexConditionalOverwritableProperty_ShouldReturnProjectContainingTargetWithOverwritableProperty()
        {
            IMsBuildProject projectFile = MsBuildProjectFile.Load("TestData/ProjectFiles/TargetWithProperties.tcsproj");

            IMsBuildTarget target = projectFile.GetChildren<IMsBuildTarget>()
                .First(t => t.Name.Equals("WithComplexConditionalOverwritableProperty"));

            IMsBuildProperty overwritableProperty =
                target.GetChildren<IMsBuildPropertyGroup>().First().GetChildren<IMsBuildProperty>()
                    .First(p => p.Name.Equals("OverwritableProperty"));

            Assert.AreEqual(true, overwritableProperty.HasPublicSetter);
        }
    }
}