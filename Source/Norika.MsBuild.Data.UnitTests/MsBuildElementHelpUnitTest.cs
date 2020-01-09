using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.MsBuild.Core.Data.Help;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class MsBuildElementHelpUnitTest
    {
        [TestMethod]
        public void Count_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            int count = elementHelp.Count;

            listMock.Verify(l => l.Count, Times.Exactly(1));
        }

        [TestMethod]
        public void IsReadOnly_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            bool isReadOnly = elementHelp.IsReadOnly;

            listMock.Verify(l => l.IsReadOnly, Times.Exactly(1));
        }

        [TestMethod]
        public void ItemAccessorGet_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            IMsBuildElementHelpParagraph obj = elementHelp[2];

            listMock.Verify(l => l[It.Is<int>(i => i.Equals(2))], Times.Exactly(1));
        }

        [TestMethod]
        public void ItemAccessorSet_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            IMsBuildElementHelpParagraph obj = elementHelp[8];

            listMock.Verify(l => l[It.Is<int>(i => i.Equals(8))], Times.Exactly(1));
        }


        [TestMethod]
        public void Clear_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            elementHelp.Clear();

            listMock.Verify(l => l.Clear(), Times.Exactly(1));
        }

        [TestMethod]
        public void Add_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IMsBuildElementHelpParagraph> itemMock = new Mock<IMsBuildElementHelpParagraph>();
            itemMock.Setup(i => i.Name).Returns("A");

            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            elementHelp.Add(itemMock.Object);

            listMock.Verify(l => l.Add(It.Is<IMsBuildElementHelpParagraph>(i => i.Name.Equals("A"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public void Remove_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IMsBuildElementHelpParagraph> itemMock = new Mock<IMsBuildElementHelpParagraph>();
            itemMock.Setup(i => i.Name).Returns("A");

            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            elementHelp.Remove(itemMock.Object);

            listMock.Verify(l => l.Remove(It.Is<IMsBuildElementHelpParagraph>(i => i.Name.Equals("A"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public void Contains_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IMsBuildElementHelpParagraph> itemMock = new Mock<IMsBuildElementHelpParagraph>();
            itemMock.Setup(i => i.Name).Returns("A");

            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            bool contains = elementHelp.Contains(itemMock.Object);

            listMock.Verify(l => l.Contains(It.Is<IMsBuildElementHelpParagraph>(i => i.Name.Equals("A"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public void Insert_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IMsBuildElementHelpParagraph> itemMock = new Mock<IMsBuildElementHelpParagraph>();
            itemMock.Setup(i => i.Name).Returns("A");

            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            elementHelp.Insert(4, itemMock.Object);

            listMock.Verify(l => l.Insert(It.Is<int>(i => i == 4),
                    It.Is<IMsBuildElementHelpParagraph>(i => i.Name.Equals("A"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public void IndexOf_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IMsBuildElementHelpParagraph> itemMock = new Mock<IMsBuildElementHelpParagraph>();
            itemMock.Setup(i => i.Name).Returns("A");

            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            int index = elementHelp.IndexOf(itemMock.Object);

            listMock.Verify(l => l.IndexOf(It.Is<IMsBuildElementHelpParagraph>(i => i.Name.Equals("A"))),
                Times.Exactly(1));
        }

        [TestMethod]
        public void RemoveAt_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            elementHelp.RemoveAt(4);

            listMock.Verify(l => l.RemoveAt(It.Is<int>(i => i == 4)),
                Times.Exactly(1));
        }

        [TestMethod]
        public void GetEnumerator_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            using (var enumerator = elementHelp.GetEnumerator())
            {
                listMock.Verify(l => l.GetEnumerator(),
                    Times.Exactly(1));
            }
        }

        [TestMethod]
        public void CopyTo_ShouldPassThroughInvocationToWrappedImplementation()
        {
            Mock<IList<IMsBuildElementHelpParagraph>> listMock = new Mock<IList<IMsBuildElementHelpParagraph>>();
            MsBuildElementHelp elementHelp = new MsBuildElementHelp(listMock.Object);

            IMsBuildElementHelpParagraph[] helpParagraphsArray = new IMsBuildElementHelpParagraph[10];

            elementHelp.CopyTo(helpParagraphsArray, 7);

            listMock.Verify(l => l.CopyTo(It.IsAny<IMsBuildElementHelpParagraph[]>(),
                It.Is<int>(i => i == 7)));
        }

        [TestMethod]
        public void Remove_WithStringValueRepresentingOneHelpParagraph_ShouldRemoveMatchingParagraph()
        {
            string matchingParagraph = "JOHNDOE";

            Mock<IMsBuildElementHelpParagraph> paragraphOne = new Mock<IMsBuildElementHelpParagraph>();
            paragraphOne.Setup(p => p.Name).Returns(matchingParagraph);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                paragraphOne.Object,
                paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);

            Assert.IsTrue(paragraphsList.Any(p => p.Name.Equals(matchingParagraph)),
                $"Not altered list should contain item with name '${matchingParagraph}'");

            sutHelp.Remove(matchingParagraph, false);

            Assert.IsFalse(paragraphsList.Any(p => p.Name.Equals(matchingParagraph)),
                $"After remove item the list should not contain a item named '{matchingParagraph}'");
        }

        [TestMethod]
        public void
            Remove_WithStringValueRepresentingMultipleHelpParagraphsAndRemoveOptionDistinctOnlyFalse_ShouldRemoveMatchingParagraph()
        {
            string matchingParagraph = "JOHNDOE";

            Mock<IMsBuildElementHelpParagraph> paragraphOne = new Mock<IMsBuildElementHelpParagraph>();
            paragraphOne.Setup(p => p.Name).Returns(matchingParagraph);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                paragraphOne.Object,
                paragraphOne.Object,
                paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);

            Assert.IsTrue(paragraphsList.Any(p => p.Name.Equals(matchingParagraph)),
                $"Not altered list should contain item with name '${matchingParagraph}'");

            sutHelp.Remove(matchingParagraph, false);

            Assert.IsFalse(paragraphsList.Any(p => p.Name.Equals(matchingParagraph)),
                $"After remove item the list should not contain a item named '{matchingParagraph}'");
        }

        [TestMethod]
        public void
            Remove_WithStringValueRepresentingMultipleHelpParagraphsAndRemoveOptionDistinctOnlyTrue_ShouldNotRemoveMatchingParagraph()
        {
            string matchingParagraph = "JOHNDOE";

            Mock<IMsBuildElementHelpParagraph> paragraphOne = new Mock<IMsBuildElementHelpParagraph>();
            paragraphOne.Setup(p => p.Name).Returns(matchingParagraph);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                paragraphOne.Object,
                paragraphOne.Object,
                paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);

            Assert.IsTrue(paragraphsList.Any(p => p.Name.Equals(matchingParagraph)),
                $"Not altered list should contain item with name '${matchingParagraph}'");

            sutHelp.Remove(matchingParagraph, true);

            Assert.IsTrue(paragraphsList.Any(p => p.Name.Equals(matchingParagraph)),
                $"After remove item the list should contain a items named '{matchingParagraph}' when remove distinct only is set to false");
        }

        [TestMethod]
        public void ContainsSection_WithStringValueNotRepresentingParagraph_ShouldReturnFalse()
        {
            Mock<IMsBuildElementHelpParagraph> paragraphOne = new Mock<IMsBuildElementHelpParagraph>();
            paragraphOne.Setup(p => p.Name).Returns("EITHERNOTRELEVANT");
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                paragraphOne.Object, paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);

            Assert.IsFalse(sutHelp.ContainsSection("SEARCH", StringComparison.Ordinal),
                "No named paragraph like the search string in the list should return false.");
        }

        [TestMethod]
        public void ContainsSection_WithStringValueWithDifferentCaseParagraphWithStringOrdinal_ShouldReturnFalse()
        {
            string searchPattern = "JOHNDOE";

            Mock<IMsBuildElementHelpParagraph> paragraphOne = new Mock<IMsBuildElementHelpParagraph>();
            paragraphOne.Setup(p => p.Name).Returns(searchPattern);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                paragraphOne.Object, paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);

            Assert.IsFalse(sutHelp.ContainsSection(searchPattern.ToLower(), StringComparison.Ordinal),
                "Same string value but different case should return false if string comparision is not set to ignore case.");
        }

        [TestMethod]
        public void
            ContainsSection_WithStringValueWithDifferentCaseParagraphWithStringOrdinalIgnoreCase_ShouldReturnTrue()
        {
            string searchPattern = "JOHNDOE";

            Mock<IMsBuildElementHelpParagraph> paragraphOne = new Mock<IMsBuildElementHelpParagraph>();
            paragraphOne.Setup(p => p.Name).Returns(searchPattern);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                paragraphOne.Object, paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);

            Assert.IsTrue(sutHelp.ContainsSection(searchPattern.ToLower(), StringComparison.OrdinalIgnoreCase),
                "Same string value with different case should return true if string comparision is set to ignore case");
        }

        [TestMethod]
        public void LookUp_WithExistentParagraphName_ShouldReturnMatchingObject()
        {
            string searchPattern = "JOHNDOE";
            string searchContent = "Content";

            Mock<IMsBuildElementHelpParagraph> searchedParagraph = new Mock<IMsBuildElementHelpParagraph>();
            searchedParagraph.Setup(p => p.Name).Returns(searchPattern);
            searchedParagraph.Setup(p => p.Content).Returns(searchContent);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                searchedParagraph.Object, paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);

            Assert.AreEqual(searchedParagraph.Object, sutHelp.LookUp(searchPattern).First());
        }

        [TestMethod]
        public void LookUp_WithExistentParagraphName_ShouldReturnMatchingObjectButNoOther()
        {
            string searchPattern = "JOHNDOE";
            string searchContent = "Content";

            Mock<IMsBuildElementHelpParagraph> searchedParagraph = new Mock<IMsBuildElementHelpParagraph>();
            searchedParagraph.Setup(p => p.Name).Returns(searchPattern);
            searchedParagraph.Setup(p => p.Content).Returns(searchContent);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                searchedParagraph.Object, paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);
            var lookedUpParagraph = sutHelp.LookUp(searchPattern);

            Assert.AreEqual(1, lookedUpParagraph.Count);
            Assert.AreEqual(searchedParagraph.Object, lookedUpParagraph.First());
        }

        [TestMethod]
        public void LookUp_WithExistentParagraphNameButDifferentCase_ShouldReturnNoMatchingObjects()
        {
            string searchPattern = "JOHNDOE";
            string searchContent = "Content";

            Mock<IMsBuildElementHelpParagraph> searchedParagraph = new Mock<IMsBuildElementHelpParagraph>();
            searchedParagraph.Setup(p => p.Name).Returns(searchPattern);
            searchedParagraph.Setup(p => p.Content).Returns(searchContent);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                searchedParagraph.Object, paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);
            var lookedUpParagraph = sutHelp.LookUp("JohnDoe");

            Assert.AreEqual(0, lookedUpParagraph.Count);
        }

        [TestMethod]
        public void
            LookUp_WithExistentParagraphNameDifferentCaseButStringComparisionIgnoreCase_ShouldReturnMatchingObject()
        {
            string searchPattern = "JOHNDOE";
            string searchContent = "Content";

            Mock<IMsBuildElementHelpParagraph> searchedParagraph = new Mock<IMsBuildElementHelpParagraph>();
            searchedParagraph.Setup(p => p.Name).Returns(searchPattern);
            searchedParagraph.Setup(p => p.Content).Returns(searchContent);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                searchedParagraph.Object, paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);
            var lookedUpParagraph = sutHelp.LookUp("JohnDoe", StringComparison.OrdinalIgnoreCase);

            Assert.AreEqual(searchedParagraph.Object, lookedUpParagraph.First());
        }

        [TestMethod]
        public void
            GetSectionContent_WithExistentParagraphNameDifferentCaseButStringComparisionIgnoreCase_ShouldReturnMatchingObjectsContent()
        {
            string searchPattern = "JOHNDOE";
            string searchContent = "Content";

            Mock<IMsBuildElementHelpParagraph> searchedParagraph = new Mock<IMsBuildElementHelpParagraph>();
            searchedParagraph.Setup(p => p.Name).Returns(searchPattern);
            searchedParagraph.Setup(p => p.Content).Returns(searchContent);
            Mock<IMsBuildElementHelpParagraph> paragraphTwo = new Mock<IMsBuildElementHelpParagraph>();
            paragraphTwo.Setup(p => p.Name).Returns("NOTRELEVANT");

            IList<IMsBuildElementHelpParagraph> paragraphsList = new List<IMsBuildElementHelpParagraph>()
            {
                searchedParagraph.Object, paragraphTwo.Object
            };

            MsBuildElementHelp sutHelp = new MsBuildElementHelp(paragraphsList);
            var sectionContent = sutHelp.GetSectionContent("JohnDoe", StringComparison.OrdinalIgnoreCase);

            Assert.AreEqual(searchContent, sectionContent);
        }
    }
}