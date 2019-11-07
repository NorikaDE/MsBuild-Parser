using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Data.UnitTests.Helper;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class RegexPropertyConditionSelfCheckIsEmptyTest
    {
        private static Regex _sut;

        private const string TestPropertyName = "TestProperty";

        [TestInitialize]
        public void InitializeSystemUnderTest()
        {
            _sut = RegexPropertyConditionSelfCheckTestHelper.GetSystemUnderTestForSelfCheckIsEmpty(TestPropertyName);
        }
        
        [TestMethod]
        public void Match_WithEmptyCondition_ShouldNotMatch()
        {
            string inputValue = "";

            Assert.IsFalse(_sut.IsMatch(inputValue), 
                "Empty condition should not match.");
        }

        [TestMethod]
        public void Match_WithConditionCheckingOnDifferentProperty_ShouldNotMatch()
        {
            string inputValue = "$(OtherProperty)=='true'";

            Assert.IsFalse(_sut.IsMatch(inputValue), 
                "Should not match if current property condition does not contain own property check.");
        }

        [TestMethod]
        public void Match_WithConditionCheckingPropertyForSpecificValue_ShouldNotMatch()
        {
            string inputValue = "$("+ TestPropertyName + ")=='true'";

            Assert.IsFalse(_sut.IsMatch(inputValue),
                "Should not match if current property condition checks for specific value different from empty or null.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingPropertyIsEmpty_ShouldMatch()
        {
            string inputValue = "$(" + TestPropertyName + ")==''";

            Assert.IsTrue(_sut.IsMatch(inputValue),
                "Checking the current property for empty should match regular expression.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingPropertyIsEmptyButOneEqualityCompareOperandOnly_ShouldNotMatch()
        {
            string inputValue = "$(" + TestPropertyName + ") = ''";

            Assert.IsFalse(_sut.IsMatch(inputValue),
                "Using one equal operand only in check should not match regular expression.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingQuotedPropertyIsEmpty_ShouldMatch()
        {
            string inputValue = "'$(" + TestPropertyName + ")'==''";

            Assert.IsTrue(_sut.IsMatch(inputValue),
                "Checking the current property for empty should match regular expression if the property is quoted.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingQuotedPropertyIsEmptyAndSingleSpaceBetweenPropertyAndOperator_ShouldMatch()
        {
            string inputValue = "'$(" + TestPropertyName + ")' ==''";

            Assert.IsTrue(_sut.IsMatch(inputValue),
                "Checking the current property for empty should match regular expression if the property is quoted and space before operator.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingQuotedPropertyIsEmptyAndMultipleSpaceBetweenPropertyAndOperator_ShouldMatch()
        {
            string inputValue = "'$(" + TestPropertyName + ")'  ==''";

            Assert.IsTrue(_sut.IsMatch(inputValue),
                "Checking the current property for empty should match regular expression if the property is quoted and space before operator.");
        }


        [TestMethod]
        public void Match_WitConditionCheckingQuotedPropertyIsEmptyAndSingleSpaceBeforeAndAfterOperator_ShouldMatch()
        {
            string inputValue = "'$(" + TestPropertyName + ")' == ''";

            Assert.IsTrue(_sut.IsMatch(inputValue),
                "Checking the current property for empty should match regular expression if the property is quoted and space before operator.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingPropertyIsEmptyWithSpaceInRightHandOperand_ShouldMatch()
        {
            string inputValue = "$(" + TestPropertyName + ") == ' '";

            Assert.IsTrue(_sut.IsMatch(inputValue),
                "Checking the current property for empty should match regular expression if the property is quoted and space before operator.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingPropertyIsEmptyAndMissingLeadingQuote_ShouldNotMatch()
        {
            string inputValue = "$(" + TestPropertyName + ")' == ''";

            Assert.IsFalse(_sut.IsMatch(inputValue),
                "Checking the current property for empty should not match if the property is not quoted correctly.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingPropertyIsEmptyAndMissingClosingQuote_ShouldNotMatch()
        {
            string inputValue = "'$(" + TestPropertyName + ") == ''";

            Assert.IsFalse(_sut.IsMatch(inputValue),
                "Checking the current property for empty should not match if the property is not quoted correctly.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingPropertyAsFirstCheckInMultipleCondition_ShouldMatch()
        {
            string inputValue = "$(" + TestPropertyName + ") == '' AND $(OtherProperty) != ''";

            Assert.IsTrue(_sut.IsMatch(inputValue),
                "Should match if condition with multiple checks contain a property self check for empty.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingPropertyAsSecondCheckInMultipleCondition_ShouldMatch()
        {
            string inputValue = "$(SpecificProperty) != '' AND $(" + TestPropertyName + ") == '' AND $(OtherProperty) != ''";

            Assert.IsTrue(_sut.IsMatch(inputValue),
                "Should match if condition with multiple checks contain a property self check for empty.");
        }

        [TestMethod]
        public void Match_WitConditionCheckingMultipleCasesButWithoutPropertySelfEmptyCheck_ShouldNotMatch()
        {
            string inputValue = "$(SpecificProperty) != '' AND $(" + TestPropertyName + ") != 'Value' AND $(OtherProperty) != ''";

            Assert.IsFalse(_sut.IsMatch(inputValue),
                "Should not match if condition does not contain a property self check for empty.");
        }
    }
}