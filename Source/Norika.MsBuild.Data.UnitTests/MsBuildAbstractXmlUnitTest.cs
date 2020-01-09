using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Types;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class MsBuildAbstractXmlUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MsBuildXmlNode_OnAccessXmlElementName_ShouldThrowException()
        {
            var xmlElementName = MsBuildXmlNode.XmlElementName;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MsBuildXmlElement_OnAccessXmlElementName_ShouldThrowException()
        {
            var xmlElementName = MsBuildXmlElement.XmlElementName;
        }
    }
}