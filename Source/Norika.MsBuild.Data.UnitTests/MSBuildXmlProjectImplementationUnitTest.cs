using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.MsBuild.Core.Data.Nodes;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class MsBuildXmlProjectImplementationUnitTest
    {
        [TestMethod]
        public void Construct_WithToolSetVersion_ShouldInitializeToolSetProperty()
        {
            string projectXml = "<Project ToolsVersion=\"15.0\"></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));
            
            Assert.AreEqual("15.0", project.MsBuildVersion);
        }
        
        [TestMethod]
        public void Construct_WithOneDefaultTarget_ShouldInitializeDefaultTargetWithCorrectValue()
        {
            string projectXml = "<Project DefaultTargets=\"TargetA\"></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));
            
            Assert.AreEqual("TargetA", project.DefaultTargets[0]);
        }
        
        [TestMethod]
        public void Construct_WithOneInitialTarget_ShouldInitializeInitialTargetWithCorrectValue()
        {
            string projectXml = "<Project InitialTargets=\"TargetA\"></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));
            
            Assert.AreEqual("TargetA", project.InitialTargets[0]);
        }
        
        [TestMethod]
        public void Construct_WithInitialTargetAndDefaultTarget_ShouldInitializeBothTargetsWithCorrectValue()
        {
            string projectXml = "<Project InitialTargets=\"TargetA\" DefaultTargets=\"TargetB\"></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));
            
            Assert.AreEqual("TargetA", project.InitialTargets[0]);
            Assert.AreEqual("TargetB", project.DefaultTargets[0]);
        }
        
        [TestMethod]
        public void Construct_WithInitialTargetAndDefaultTargetAndToolsVersion_ShouldInitializeBothTargetsAndToolsVersionWithCorrectValue()
        {
            string projectXml = "<Project InitialTargets=\"TargetA\" DefaultTargets=\"TargetB\" ToolsVersion=\"15.0\"></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));
            
            Assert.AreEqual("TargetA", project.InitialTargets[0]);
            Assert.AreEqual("TargetB", project.DefaultTargets[0]);
            Assert.AreEqual("15.0", project.MsBuildVersion);
        }
        
        [TestMethod]
        public void Construct_WithTwoInitialTargets_ShouldInitializeSecondInitialTargetWithCorrectValue()
        {
            string projectXml = "<Project InitialTargets=\"TargetA;TargetB\"></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));
            
            Assert.AreEqual("TargetB", project.InitialTargets[1]);
        }
        
        [TestMethod]
        public void Construct_WithTwoDefaultTarget_ShouldInitializeSecondDefaultTargetWithCorrectValue()
        {
            string projectXml = "<Project DefaultTargets=\"TargetA;TargetB\"></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));
            
            Assert.AreEqual("TargetB", project.DefaultTargets[1]);
        }

        [TestMethod]
        public void Construct_WithoutToolsVersion_ShouldInitializeToolSetWithNull()
        {
            string projectXml = "<Project></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));
            
            Assert.IsNull(project.MsBuildVersion);
        }
        
        [TestMethod]
        public void GetChildren_WithOnePropertyGroup_ReturnCorrectProperty()
        {
            string projectXml = "<Project><PropertyGroup><TestProperty>TestValue</TestProperty></PropertyGroup></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));

            IMsBuildPropertyGroup propertyGroup = project.GetChildren<IMsBuildPropertyGroup>().First();
            
            Assert.AreEqual("TestProperty", propertyGroup[0].Name);
            Assert.AreEqual("TestValue", propertyGroup[0].Value);
        }
        
        [TestMethod]
        public void GetChildren_WithOneTarget_ReturnCorrectTarget()
        {
            string projectXml = "<Project><Target Name=\"TestTargetA\"></Target></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));

            IMsBuildTarget target = project.GetChildren<IMsBuildTarget>().First();
            
            Assert.AreEqual("TestTargetA", target.Name);
        }
        
        [TestMethod]
        public void GetChildren_WithTwoTargets_ReturnCorrectTargetsWithName()
        {
            string projectXml = "<Project><Target Name=\"TestTargetA\"></Target><Target Name=\"TestTargetB\"></Target></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));

            IList<IMsBuildTarget> target = project.GetChildren<IMsBuildTarget>();
            
            Assert.AreEqual("TestTargetA", target[0].Name);
            Assert.AreEqual("TestTargetB", target[1].Name);
        }
        
        [TestMethod]
        public void GetChildren_WithTargetAndPropertyGroup_ReturnCorrectTargetAndPropertyGroupContent()
        {
            string projectXml = "<Project><PropertyGroup><TestProperty>Value</TestProperty></PropertyGroup><Target Name=\"TestTargetA\"></Target></Project>";
            
            IMsBuildProject project = new MsBuildXmlProjectImplementation(CreateFromString(projectXml));

            IList<IMsBuildTarget> target = project.GetChildren<IMsBuildTarget>();
            
            Assert.AreEqual("TestTargetA", target[0].Name);

            IMsBuildPropertyGroup propertyGroup = project.GetChildren<IMsBuildPropertyGroup>().First();
            
            Assert.AreEqual("Value", propertyGroup.First().Value);
            Assert.AreEqual("TestProperty", propertyGroup.First().Name);
        }

        [TestMethod]
        public void Count_WithTenItems_ShouldReturnValueTen()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();
            
            Assert.AreEqual(10, project.Count);
        }
        
        [TestMethod]
        public void Remove_LastItem_ShouldRemoveLastItem()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();

            project.Remove(project.Last());
            
            Assert.AreEqual("Target9", project.GetChildren<IMsBuildTarget>().Last().Name);
        }
        
        [TestMethod]
        public void SetAccessor_NewItem_ShouldReplaceExistentItem()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();

            Mock<IMsBuildTarget> targetMock = new Mock<IMsBuildTarget>();
            targetMock.Setup(x => x.Name).Returns("Target99");

            project[project.Count - 1] = targetMock.Object;
            
            Assert.AreEqual("Target99", project.GetChildren<IMsBuildTarget>().Last().Name);
        }
        
          
        [TestMethod]
        public void CopyTo_Array_ShouldCopyAllValuesToArray()
        {
            string inputValue = "<Project></Project>";

            IMsBuildTarget[] array = new IMsBuildTarget[20];
            
            MsBuildXmlProjectImplementation projectImplementation = 
                new MsBuildXmlProjectImplementation(CreateFromString(inputValue));

            for (int i = 0; i <= 10; i++)
            {
                Mock<IMsBuildTarget> mock = new Mock<IMsBuildTarget>();
                mock.Setup(m => m.Name).Returns($"Target{i}");

                projectImplementation.Add(mock.Object);   
            }

            projectImplementation.CopyTo(array, 0);
            
            Assert.AreEqual("Target0", array[0].Name);
        }
        
        [TestMethod]
        public void Clear_OnList_ShouldRemoveAllItems()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();

            project.Clear();
            
            Assert.AreEqual(0, project.Count);
        }
        
        [TestMethod]
        public void Contains_ExistingItem_ShouldReturnTrue()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();

            IMsBuildTarget target = project.GetChildren<IMsBuildTarget>().First();
            
            Assert.IsTrue(project.Contains(target));
        }
        
        [TestMethod]
        public void Contains_NotExistingItem_ShouldReturnFalse()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();

            Mock<IMsBuildTarget> targetMock = new Mock<IMsBuildTarget>();
            targetMock.Setup(x => x.Name).Returns("Target99");
            
            Assert.IsFalse(project.Contains(targetMock.Object));
        }
        
        [TestMethod]
        public void InsertAt_Position5_ShouldAddItemAtPosition5()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();

            Mock<IMsBuildTarget> targetMock = new Mock<IMsBuildTarget>();
            targetMock.Setup(x => x.Name).Returns("Target99");
            project.Insert(5, targetMock.Object);
            
            Assert.AreEqual("Target99", project.GetChildren<IMsBuildTarget>()[5].Name);
        }
        
        [TestMethod]
        public void IsReadOnly_OnList_ShouldReturnFalse()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();
            
            Assert.IsFalse(project.IsReadOnly);
        }
        
        [TestMethod]
        public void Iterate_OverAllProjectWithAllIMsBuildTargetElements_ShouldGetAllInstancesOfIMsBuildTarget()
        {
            IEnumerable<IMsBuildElement> project = CreateProjectWithMockedContent();
            foreach (IMsBuildElement buildElement in project)
            {
                Assert.IsInstanceOfType(buildElement, typeof(IMsBuildTarget));
            }
        }
        
        [TestMethod]
        public void RemoveAt_LastIndex_ShouldRemoveLastItemAndReturnTarget9AsLastOne()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();

            project.RemoveAt(9);

            Assert.AreEqual("Target9", project.GetChildren<IMsBuildTarget>().Last().Name);
        }
        
        [TestMethod]
        public void IndexOf_ExistingItemAtIndex5_ShouldReturn5()
        {
            MsBuildXmlProjectImplementation project = CreateProjectWithMockedContent();

            IMsBuildTarget target = project.GetChildren<IMsBuildTarget>()[5];

            Assert.AreEqual(5, project.IndexOf(target));
        }
        
        
        
        private XmlDocument CreateFromString(string elementString)
        {
            XmlDocument document = new XmlDocument();
            
            document.LoadXml(elementString);

            return document;
        }

        private MsBuildXmlProjectImplementation CreateProjectWithMockedContent()
        {
            MsBuildXmlProjectImplementation projectImplementation = 
                new MsBuildXmlProjectImplementation(CreateFromString("<Project></Project>"));

            for (int i = 1; i <= 10; i++)
            {
                Mock<IMsBuildTarget> targetMock = new Mock<IMsBuildTarget>();
                targetMock.Setup(t => t.Name).Returns($"Target{i}");
                projectImplementation.Add(targetMock.Object);
            }

            return projectImplementation;
        }
    }
}