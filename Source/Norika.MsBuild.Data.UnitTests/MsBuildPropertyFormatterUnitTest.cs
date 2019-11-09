using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Utilities;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class MsBuildPropertyFormatterUnitTest
    {
        private IFormatProvider _sutFormatter;

        [TestInitialize]
        public void InitializeTest()
        {
            _sutFormatter = MsBuildStringUtilities.FormatProvider;
        }

        [TestMethod]
        public void Format_AsPropertyWithStringInput_ShouldReturnCorrectFormattedString()
        {
            string inputValue = "Property";

            string outputValue = string.Format(_sutFormatter, "{0:Property}", arg0: inputValue);

            Assert.AreEqual("$(Property)", outputValue);
        }

        [TestMethod]
        public void Format_AsItemGroupWithStringInput_ShouldReturnCorrectFormattedString()
        {
            string inputValue = "ItemGroup";

            string outputValue = string.Format(_sutFormatter, "{0:ItemGroup}", arg0: inputValue);

            Assert.AreEqual("@(ItemGroup)", outputValue);
        }

        [TestMethod]
        public void Format_AsItemGroupWithEmptyStringInput_ShouldReturnSameValueAsInputString()
        {
            string inputValue = "";

            string outputValue = string.Format(_sutFormatter, "{0:ItemGroup}", arg0: inputValue);

            Assert.AreEqual(inputValue, outputValue);
        }

        [TestMethod]
        public void Format_WithoutFormatStringInput_ShouldReturnEmptyString()
        {
            string outputValue = string.Format(_sutFormatter, "");

            Assert.AreEqual("", outputValue);
        }

        [TestMethod]
        public void Format_WithNullArgument_ShouldReturnFormatString()
        {
            string outputValue = string.Format(_sutFormatter, "Hallo {0:Property}", arg0: null);

            Assert.AreEqual("Hallo ", outputValue);
        }

        [TestMethod]
        public void Format_AsMultipleTypesWithCorrectInput_ShouldReturnProperlyFormattedString()
        {
            string inputValue1 = "ItemGroup";
            string inputValue2 = "Property";

            string outputValue = string.Format(_sutFormatter, "{0:ItemGroup} is not {1:Property}", inputValue1,
                inputValue2);

            Assert.AreEqual("@(ItemGroup) is not $(Property)", outputValue);
        }

        [TestMethod]
        public void GetFormat_WithStringTypeInput_ShouldReturnNull()
        {
            MsBuildStringFormatProvider provider = new MsBuildStringFormatProvider();

            Assert.IsNull(provider.GetFormat(typeof(string)));
        }

        [TestMethod]
        public void Format_WithDateTimeFormatProvider_ShouldReturnEmptyString()
        {
            MsBuildStringFormatter formatter = new MsBuildStringFormatter();

            Assert.AreEqual(string.Empty, formatter.Format("{0}", "Test", DateTimeFormatInfo.CurrentInfo));
        }

        [TestMethod]
        public void Format_WithEmptyFormatString_ShouldReturnArgument()
        {
            MsBuildStringFormatter formatter = new MsBuildStringFormatter();

            Assert.AreEqual("Test", formatter.Format(string.Empty, "Test", _sutFormatter));
        }
    }
}