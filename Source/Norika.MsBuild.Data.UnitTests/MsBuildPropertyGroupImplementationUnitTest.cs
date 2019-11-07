using System;
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
    public class MsBuildPropertyGroupImplementationUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_WithNullElement_ShouldThrowArgumentNullException()
        {
            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(null);
        }
        
        [TestMethod]
        public void Construct_WithOnePropertyNameTestAndValueContent_ShouldInitializeOnePropertyWithNameTestAndValueContent()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Assert.AreEqual("Test", propertyGroupImplementation[0].Name);
            Assert.AreEqual("Content", propertyGroupImplementation[0].Value);
        }
        
        [TestMethod]
        public void Count_WithOneProperty_ShouldReturnOne()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Assert.AreEqual(1, propertyGroupImplementation.Count);
       
        }
        
        [TestMethod]
        public void IsReadOnly_WithOneProperty_ShouldReturnFalse()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Assert.AreEqual(false, propertyGroupImplementation.IsReadOnly);
       
        }
        
        [TestMethod]
        public void AccessorGetValue_WithOneProperty_ShouldBeAccessible()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Assert.AreEqual("Content", propertyGroupImplementation[0].Value);
       
        }
        
        [TestMethod]
        public void AccessorSetValue_WithOneProperty_ShouldBeSettable()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Mock<IMsBuildProperty> mock = new Mock<IMsBuildProperty>();
            mock.Setup(m => m.Value).Returns("OverwrittenContent");
            mock.Setup(m => m.Name).Returns("OverwrittenTest");

            propertyGroupImplementation[0] = mock.Object;
            
            Assert.AreEqual("OverwrittenContent", propertyGroupImplementation[0].Value);
       
        }
        
        [TestMethod]
        public void Add_WithOneProperty_ShouldAddItemToList()
        {
            string inputValue = "<PropertyGroup></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Mock<IMsBuildProperty> mock = new Mock<IMsBuildProperty>();
            mock.Setup(m => m.Value).Returns("OverwrittenContent");
            mock.Setup(m => m.Name).Returns("OverwrittenTest");

            propertyGroupImplementation.Add(mock.Object);
            
            Assert.AreEqual("OverwrittenContent", propertyGroupImplementation[0].Value);
       
        }
        
        [TestMethod]
        public void Remove_FirstEntry_ShouldRemoveFirstEntry()
        {
            string inputValue = "<PropertyGroup></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));

            for (int i = 1; i < 10; i++)
            {
                Mock<IMsBuildProperty> mock = new Mock<IMsBuildProperty>();
                mock.Setup(m => m.Value).Returns($"OverwrittenContent{i}");
                mock.Setup(m => m.Name).Returns($"OverwrittenTest{i}");

                propertyGroupImplementation.Add(mock.Object);   
            }

            propertyGroupImplementation.Remove(propertyGroupImplementation[0]);

            Assert.AreEqual("OverwrittenContent2", propertyGroupImplementation[0].Value);
        }
        
        [TestMethod]
        public void Remove_LastEntry_ShouldRemoveLastEntry()
        {
            string inputValue = "<PropertyGroup></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));

            for (int i = 1; i <= 10; i++)
            {
                Mock<IMsBuildProperty> mock = new Mock<IMsBuildProperty>();
                mock.Setup(m => m.Value).Returns($"OverwrittenContent{i}");
                mock.Setup(m => m.Name).Returns($"OverwrittenTest{i}");

                propertyGroupImplementation.Add(mock.Object);   
            }

            propertyGroupImplementation.RemoveAt(propertyGroupImplementation.Count - 1);

            Assert.AreEqual("OverwrittenContent9", 
                propertyGroupImplementation[propertyGroupImplementation.Count - 1].Value);
       
        }
        
          
        [TestMethod]
        public void IndexOf_LastEntryOfTenItems_ShouldReturnValueTen()
        {
            string inputValue = "<PropertyGroup></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));

            for (int i = 0; i <= 10; i++)
            {
                Mock<IMsBuildProperty> mock = new Mock<IMsBuildProperty>();
                mock.Setup(m => m.Value).Returns($"OverwrittenContent{i}");
                mock.Setup(m => m.Name).Returns($"OverwrittenTest{i}");

                propertyGroupImplementation.Add(mock.Object);   
            }

            Assert.AreEqual(10, 
                propertyGroupImplementation.IndexOf(propertyGroupImplementation.Last()));
        }
        
        [TestMethod]
        public void CopyTo_Array_ShouldCopyAllValuesToArray()
        {
            string inputValue = "<PropertyGroup></PropertyGroup>";

            IMsBuildProperty[] array = new IMsBuildProperty[20];
            
            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));

            for (int i = 0; i <= 10; i++)
            {
                Mock<IMsBuildProperty> mock = new Mock<IMsBuildProperty>();
                mock.Setup(m => m.Value).Returns($"OverwrittenContent{i}");
                mock.Setup(m => m.Name).Returns($"OverwrittenTest{i}");

                propertyGroupImplementation.Add(mock.Object);   
            }

            propertyGroupImplementation.CopyTo(array, 0);
            
            Assert.AreEqual("OverwrittenContent0", array[0].Value);
        }
        
        [TestMethod]
        public void Insert_InMiddlePosition_ShouldAddItemInFifthPosition()
        {
            string inputValue = "<PropertyGroup></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));

            for (int i = 0; i <= 10; i++)
            {
                Mock<IMsBuildProperty> mock = new Mock<IMsBuildProperty>();
                mock.Setup(m => m.Value).Returns($"OverwrittenContent{i}");
                mock.Setup(m => m.Name).Returns($"OverwrittenTest{i}");

                propertyGroupImplementation.Add(mock.Object);   
            }
            
            Mock<IMsBuildProperty> mockInsert = new Mock<IMsBuildProperty>();
            mockInsert.Setup(m => m.Value).Returns($"OverwrittenContent99");
            mockInsert.Setup(m => m.Name).Returns($"OverwrittenTest99");
            
            propertyGroupImplementation.Insert(5, mockInsert.Object);

            Assert.AreEqual("OverwrittenContent99", propertyGroupImplementation[5].Value);
        }
        
        [TestMethod]
        public void Clear_WithOneProperty_ShouldRemoveAllEntries()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            propertyGroupImplementation.Clear();
            
            Assert.AreEqual(0, propertyGroupImplementation.Count);
       
        }
        
        [TestMethod]
        public void Contains_WithExistentProperty_ShouldReturnTrue()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));

            IMsBuildProperty property = propertyGroupImplementation.First();
            
            Assert.IsTrue(propertyGroupImplementation.Contains(property));
       
        }
        
        [TestMethod]
        public void Enumerate_WithTwoEntries_ShouldEnumerateAllEntries()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test><Test2>Content</Test2></PropertyGroup>";

            IEnumerable<IMsBuildProperty> propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));

            foreach (IMsBuildProperty property in propertyGroupImplementation)
            {
                Assert.AreEqual("Content", property.Value);
            }
        }

        
        [TestMethod]
        public void Construct_WithOnePropertyNotConditional_ShouldInitializeOnePropertNotConditional()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Assert.IsFalse(propertyGroupImplementation[0].IsConditional);
        }
        
        [TestMethod]
        public void Construct_WithTwoProperties_ShouldInitializeTwoProperties()
        {
            string inputValue = "<PropertyGroup><Test>Content</Test><Test2>Content</Test2></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Assert.AreEqual( "Test", propertyGroupImplementation[0].Name);
            Assert.AreEqual("Test2", propertyGroupImplementation[1].Name);
        }
        
        [TestMethod]
        public void Construct_WithoutProperties_ShouldInitializeNoProperties()
        {
            string inputValue = "<PropertyGroup></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Assert.AreEqual(0, propertyGroupImplementation.Count);
        }
        
        [TestMethod]
        public void Construct_WithPropertyAndCondition_ShouldInitializePropertyHasConditionTrue()
        {
            string inputValue = "<PropertyGroup><Test Condition=\"$(Conditioner)=='Condition'\">Value</Test></PropertyGroup>";

            MsBuildXmlPropertyGroupImplementation propertyGroupImplementation = 
                new MsBuildXmlPropertyGroupImplementation(CreateFromString(inputValue));
            
            Assert.IsTrue(propertyGroupImplementation[0].IsConditional);
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