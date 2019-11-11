using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Norika.MsBuild.Core.Data.Converter;
using Norika.MsBuild.Model.Interfaces;
using Norika.Xml.CommentBasedHelp.Data.Interfaces;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class XmlHelpToMsBuildElementHelpConverter
    {
        [TestMethod]
        public void ToMsBuildElementHelp_WithXmHelpParagraphWithName_ShouldReturnObjectWithCorrectName()
        {
            string name = "Name";

            Mock<IXmlCommentHelpParagraph> xmlHelpParagraph = new Mock<IXmlCommentHelpParagraph>();
            xmlHelpParagraph.Setup(hp => hp.Name).Returns(name);
            xmlHelpParagraph.Setup(hp => hp.Content).Returns(new List<string>());

            IMsBuildElementHelpParagraph converted = XmlHelpExtension.ToMsBuildElementHelp(xmlHelpParagraph.Object);

            Assert.AreEqual(name, converted.Name);
        }

        [TestMethod]
        public void ToMsBuildElementHelp_WithXmHelpParagraphWithContent_ShouldReturnObjectWithCorrectContent()
        {
            string content = "Content";

            Mock<IXmlCommentHelpParagraph> xmlHelpParagraph = new Mock<IXmlCommentHelpParagraph>();
            xmlHelpParagraph.Setup(hp => hp.Content).Returns(new List<string>() {content});

            IMsBuildElementHelpParagraph converted = XmlHelpExtension.ToMsBuildElementHelp(xmlHelpParagraph.Object);

            Assert.AreEqual(content, converted.Content);
        }

        [TestMethod]
        public void
            ToMsBuildElementHelp_WithXmHelpParagraphWithAdditionalContent_ShouldReturnObjectWithCorrectAdditionalContent()
        {
            string additional = "AdditionalContent";

            Mock<IXmlCommentHelpParagraph> xmlHelpParagraph = new Mock<IXmlCommentHelpParagraph>();
            xmlHelpParagraph.Setup(hp => hp.Additional).Returns(additional);
            xmlHelpParagraph.Setup(hp => hp.Content).Returns(new List<string>());

            IMsBuildElementHelpParagraph converted = XmlHelpExtension.ToMsBuildElementHelp(xmlHelpParagraph.Object);

            Assert.AreEqual(additional, converted.Additional);
        }
    }
}