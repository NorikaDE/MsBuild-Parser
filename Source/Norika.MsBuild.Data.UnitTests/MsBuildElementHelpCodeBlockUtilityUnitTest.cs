using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Help;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class MsBuildElementHelpCodeBlockUtilityUnitTest
    {
        [TestMethod]
        public void Parse_WithValidXmlInput_ShouldReturnObjectWithLanguageXml()
        {
            string xmlInput = "<xml>test</xml>";

            MsBuildHelpElementCodeBlock codeBlock = MsBuildElementHelpCodeBlockUtility.Parse(xmlInput);

            Assert.AreEqual(MsBuildHelpCodeBlockLanguage.Xml, codeBlock.Language);
        }

        [TestMethod]
        public void Parse_WithBatchInput_ShouldReturnObjectWithLanguageSh()
        {
            string xmlInput = "msbuild test.targets /t:CallTest";

            MsBuildHelpElementCodeBlock codeBlock = MsBuildElementHelpCodeBlockUtility.Parse(xmlInput);

            Assert.AreEqual(MsBuildHelpCodeBlockLanguage.Sh, codeBlock.Language);
        }

        [TestMethod]
        public void Parse_WithValidEncodedXmlCommentInput_ShouldDecodeStringContent()
        {
            string xmlInput = "<!~~ Test ~~>";

            MsBuildHelpElementCodeBlock codeBlock = MsBuildElementHelpCodeBlockUtility.Parse(xmlInput);

            Assert.AreEqual("<!-- Test -->", codeBlock.Content);
        }

        [TestMethod]
        public void Decode_WithStartAndEndIdentifierWithTildeAsMinus_ShouldReplaceTildeWithMinus()
        {
            string inputString = "<!~~ This is an encoded xml comment ~~>";
            string expectedString = "<!-- This is an encoded xml comment -->";

            Assert.AreEqual(expectedString, MsBuildElementHelpCodeBlockUtility.Decode(inputString));
        }

        [TestMethod]
        public void Decode_WithStartAndEndIdentifierWithPowerShellCommentConvention_ShouldReplaceWithProperXmlComment()
        {
            string inputString = "<# This is an encoded xml comment #>";
            string expectedString = "<!-- This is an encoded xml comment -->";

            Assert.AreEqual(expectedString, MsBuildElementHelpCodeBlockUtility.Decode(inputString));
        }

        [TestMethod]
        public void Decode_WithStartAndEndIdentifierRepresentedByHtmlEncoded_ShouldDecode()
        {
            string inputString = "&lt;!-- This is an encoded xml comment --&gt;";
            string expectedString = "<!-- This is an encoded xml comment -->";

            Assert.AreEqual(expectedString, MsBuildElementHelpCodeBlockUtility.Decode(inputString));
        }

        [TestMethod]
        public void Decode_WithEmptyInputValue_ShouldReturnEmptyValue()
        {
            Assert.AreEqual(string.Empty, MsBuildElementHelpCodeBlockUtility.Decode(string.Empty));
        }

        [TestMethod]
        public void GetLanguage_WithValidXmlInputString_ShouldReturnXml()
        {
            string inputString = "<xml>Test</xml>";

            Assert.AreEqual(MsBuildHelpCodeBlockLanguage.Xml,
                MsBuildElementHelpCodeBlockUtility.GetLanguage(inputString));
        }

        [TestMethod]
        public void GetLanguage_WithValidXmlInputStringStartingWithComment_ShouldReturnXml()
        {
            string inputString = "<!-- This is a xml comment --><xml>Test</xml>";

            Assert.AreEqual(MsBuildHelpCodeBlockLanguage.Xml,
                MsBuildElementHelpCodeBlockUtility.GetLanguage(inputString));
        }

        [TestMethod]
        public void GetLanguage_WithValidXmlInputStringEndingWithComment_ShouldReturnXml()
        {
            string inputString = "<xml>Test</xml><!-- This is a xml comment -->";

            Assert.AreEqual(MsBuildHelpCodeBlockLanguage.Xml,
                MsBuildElementHelpCodeBlockUtility.GetLanguage(inputString));
        }

        [TestMethod]
        public void GetLanguage_WithInvalidXmlInputStringEndingWithComment_ShouldReturnBatch()
        {
            string inputString = "<xml>Test<xml><!-- This is a xml comment -->";

            Assert.AreEqual(MsBuildHelpCodeBlockLanguage.Sh,
                MsBuildElementHelpCodeBlockUtility.GetLanguage(inputString));
        }

        [TestMethod]
        public void GetLanguage_WithMsBuildCommandLineCall_ShouldReturnBatch()
        {
            string inputString = "msbuild.exe test.targets /t:Print /p:File='C:/doc.txt'";

            Assert.AreEqual(MsBuildHelpCodeBlockLanguage.Sh,
                MsBuildElementHelpCodeBlockUtility.GetLanguage(inputString));
        }

        [TestMethod]
        public void FormatXml_WithValidXmlInputStringInOneRow_ShouldReturnFormattedXml()
        {
            string inputString = "<!-- This is a xml comment --><xml><node id='1'>Test</node></xml>";
            string expectedString = "<!-- This is a xml comment -->\n<xml>\n  <node\n    id=\"1\">Test</node>\n</xml>";

            Assert.AreEqual(expectedString,
                MsBuildElementHelpCodeBlockUtility.FormatXml(inputString));
        }

        [TestMethod]
        public void FormatXml_WithMultipleRootNodeXmlInputStringInOneRow_ShouldReturnFormattedXml()
        {
            string inputString = "<!-- This is a xml comment --><xml><node id='1'>Test</node></xml><xml><test/></xml>";
            string expectedString =
                "<!-- This is a xml comment -->\n<xml>\n  <node\n    id=\"1\">Test</node>\n</xml>\n<xml>\n  <test />\n</xml>";

            Assert.AreEqual(expectedString,
                MsBuildElementHelpCodeBlockUtility.FormatXml(inputString));
        }

        [TestMethod]
        public void FormatXml_WithValidXmlInputStringInOneRowAndCommentOnTheEnd_ShouldReturnFormattedXml()
        {
            string inputString = "<xml><node id='1'>Test</node></xml><!-- This is a xml comment -->";
            string expectedString = "<xml>\n  <node\n    id=\"1\">Test</node>\n</xml>\n<!-- This is a xml comment -->";

            Assert.AreEqual(expectedString,
                MsBuildElementHelpCodeBlockUtility.FormatXml(inputString));
        }

        [TestMethod]
        public void IsStringXml_WithOrdinalStringInput_ShouldReturnFalse()
        {
            string input = "This is an ordinal string value";
            Assert.IsFalse(MsBuildElementHelpCodeBlockUtility.IsStringXml(input));
        }

        [TestMethod]
        public void IsStringXml_WithEmptyInput_ShouldReturnFalse()
        {
            string input = "";
            Assert.IsFalse(MsBuildElementHelpCodeBlockUtility.IsStringXml(input));
        }

        [TestMethod]
        public void IsStringXml_WithNullInput_ShouldReturnFalse()
        {
            string input = null;
            Assert.IsFalse(MsBuildElementHelpCodeBlockUtility.IsStringXml(input));
        }

        [TestMethod]
        public void IsStringXml_WithClosingXmlComment_ShouldReturnTrue()
        {
            string input = "<TestValue></TestValue><!~~ This is a decoded comment ~~>";
            Assert.IsTrue(MsBuildElementHelpCodeBlockUtility.IsStringXml(input));
        }

        [TestMethod]
        public void IsStringXml_WithLeadingXmlComment_ShouldReturnTrue()
        {
            string input = "<!-- This is a decoded comment --><TestValue></TestValue>";
            Assert.IsTrue(MsBuildElementHelpCodeBlockUtility.IsStringXml(input));
        }

        [TestMethod]
        public void IsStringXml_WithOrdinalStringInputLookingLikeXml_ShouldReturnFalse()
        {
            string input = "<xml><xml>bullshit<xml><xml>";
            Assert.IsFalse(MsBuildElementHelpCodeBlockUtility.IsStringXml(input));
        }

        [TestMethod]
        public void IsStringXml_WithXmlFragmentInput_ShouldReturnTrue()
        {
            string input = "<element /><xml>bullshit</xml>";
            Assert.IsTrue(MsBuildElementHelpCodeBlockUtility.IsStringXml(input));
        }

        [TestMethod]
        public void IsStringXml_WithXmlInput_ShouldReturnTrue()
        {
            string input = "<root><element /><xml>bullshit</xml></root>";
            Assert.IsTrue(MsBuildElementHelpCodeBlockUtility.IsStringXml(input));
        }

        [TestMethod]
        public void IsStringXml_WithTwoRootNodesInput_ShouldReturnTrue()
        {
            string input = "<root><element /><xml>bullshit</xml></root><root><test /></root>";
            Assert.IsTrue(MsBuildElementHelpCodeBlockUtility.IsStringXml(input));
        }
    }
}