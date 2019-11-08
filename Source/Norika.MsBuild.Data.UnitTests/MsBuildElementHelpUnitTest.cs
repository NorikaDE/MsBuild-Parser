using System.Collections;
using System.Collections.Generic;
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
    }
}