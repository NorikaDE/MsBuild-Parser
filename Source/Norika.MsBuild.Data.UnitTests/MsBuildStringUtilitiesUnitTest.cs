using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Utilities;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class MsBuildStringUtilitiesUnitTest
    {
        [TestMethod]
        public void SplitByDefaultSeparator_WithSemicolonSeparatedListWithTwoEntries_ShouldReturnListWithTwoEntries()
        {
            string inputValue = "A;B";

            IList<string> separatedList = MsBuildStringUtilities.SplitByDefaultSeparator(inputValue);

            Assert.AreEqual(2, separatedList.Count,
                $"Input value '{inputValue}' should return list with two entries.");
        }

        [TestMethod]
        public void
            SplitByDefaultSeparator_WithSemicolonSeparatedListWithTwoEntries_ShouldReturnListContainingCorrectEntries()
        {
            string inputValue = "A;B";

            IList<string> separatedList = MsBuildStringUtilities.SplitByDefaultSeparator(inputValue);

            Assert.AreEqual("A", separatedList[0],
                $"The first list value should contain the first input value 'A'");

            Assert.AreEqual("B", separatedList[1],
                $"The first list value should contain the first input value 'B'");
        }

        [TestMethod]
        public void
            SplitByDefaultSeparator_WithSemicolonSeparatedListWithTwoEntriesAndClosingSemicolon_ShouldReturnListWithTwoEntriesAndRemoveEmptyEntry()
        {
            string inputValue = "A;B;";

            IList<string> separatedList = MsBuildStringUtilities.SplitByDefaultSeparator(inputValue);

            Assert.AreEqual(2, separatedList.Count,
                $"Input value '{inputValue}' should return list with two entries.");
        }

        [TestMethod]
        public void
            SplitByDefaultSeparator_WithSemicolonSeparatedListWithTwoEntriesAndOpeningSemicolon_ShouldReturnListWithTwoEntriesAndRemoveEmptyEntry()
        {
            string inputValue = ";A;B";

            IList<string> separatedList = MsBuildStringUtilities.SplitByDefaultSeparator(inputValue);

            Assert.AreEqual(2, separatedList.Count,
                $"Input value '{inputValue}' should return list with two entries.");
        }

        [TestMethod]
        public void
            SplitByDefaultSeparator_WithSemicolonSeparatedListWithTwoEntriesAndOpeningAndClosingSemicolon_ShouldReturnListWithTwoEntriesAndRemoveEmptyEntry()
        {
            string inputValue = ";A;B;";

            IList<string> separatedList = MsBuildStringUtilities.SplitByDefaultSeparator(inputValue);

            Assert.AreEqual(2, separatedList.Count,
                $"Input value '{inputValue}' should return list with two entries.");
        }

        [TestMethod]
        public void SplitByDefaultSeparator_WithEmptyInputValue_ShouldReturnEmptyList()
        {
            string inputValue = "";

            IList<string> separatedList = MsBuildStringUtilities.SplitByDefaultSeparator(inputValue);

            Assert.AreEqual(0, separatedList.Count,
                $"Input value '{inputValue}' should return list with zero entries.");
        }

        [TestMethod]
        public void SplitByDefaultSeparator_WithNullInputValue_ShouldReturnEmptyList()
        {
            string inputValue = null;

            IList<string> separatedList = MsBuildStringUtilities.SplitByDefaultSeparator(inputValue);

            Assert.AreEqual(0, separatedList.Count,
                "Input value 'null' should return list with zero entries.");
        }

        [TestMethod]
        public void SplitByNewLine_WithoutNewLineSeparator_ShouldReturnListWithOneEntry()
        {
            string inputValue = "This is a line";

            IList<string> separatedList = MsBuildStringUtilities.SplitByNewLine(inputValue);

            Assert.AreEqual(1, separatedList.Count,
                "A string without new line in content should return a list with one entry.");
        }

        [TestMethod]
        public void SplitByNewLine_WithoutNewLineSeparator_ShouldReturnLineContentAsIs()
        {
            string inputValue = "This is a line";

            IList<string> separatedList = MsBuildStringUtilities.SplitByNewLine(inputValue);

            Assert.AreEqual(inputValue, separatedList[0],
                "A string without new line in content should return the same value as input string value.");
        }

        [TestMethod]
        public void SplitByNewLine_WithTwoLines_ShouldReturnListWithTwoLines()
        {
            string lineA = "This is line A";
            string lineB = "This is line B";

            string inputValue = $"{lineA}\n{lineB}";

            IList<string> separatedList = MsBuildStringUtilities.SplitByNewLine(inputValue);

            Assert.AreEqual(2, separatedList.Count,
                "A string with one line separator should return a list with two entries");
        }

        [TestMethod]
        public void SplitByNewLine_WithTwoLines_ShouldReturnCorrectEntriesInList()
        {
            string lineA = "This is line A";
            string lineB = "This is line B";

            string inputValue = $"{lineA}\n{lineB}";

            IList<string> separatedList = MsBuildStringUtilities.SplitByNewLine(inputValue);

            Assert.AreEqual(lineA, separatedList[0],
                "The split lines should separated correct.");
            Assert.AreEqual(lineB, separatedList[1],
                "The split lines should separated correct.");
        }

        [TestMethod]
        public void SplitByNewLine_WithTwoSeparatorsButOnlyTwoLines_ShouldReturnTwoLines()
        {
            string lineA = "This is line A";
            string lineB = "This is line B";

            string inputValue = $"{lineA}\n{lineB}\n";

            IList<string> separatedList = MsBuildStringUtilities.SplitByNewLine(inputValue);

            Assert.AreEqual(2, separatedList.Count,
                "A string with two line separator but two lines should return a list with two entries");
        }

        [TestMethod]
        public void SplitByNewLine_WithTwoSeparatorsAndOpeningSeparator_ShouldReturnTwoLines()
        {
            string lineA = "This is line A";
            string lineB = "This is line B";

            string inputValue = $"\n{lineA}\n{lineB}";

            IList<string> separatedList = MsBuildStringUtilities.SplitByNewLine(inputValue);

            Assert.AreEqual(2, separatedList.Count,
                "A string with two line separator but two lines should return a list with two entries");
        }

        [TestMethod]
        public void SplitByNewLine_WithTwoSeparatorsAndOpeningAndClosingSeparator_ShouldReturnTwoLines()
        {
            string lineA = "This is line A";
            string lineB = "This is line B";

            string inputValue = $"\n{lineA}\n{lineB}\n";

            IList<string> separatedList = MsBuildStringUtilities.SplitByNewLine(inputValue);

            Assert.AreEqual(2, separatedList.Count,
                "A string with two line separator but two lines should return a list with two entries");
        }

        [TestMethod]
        public void SplitByNewLine_WithNullInputValue_ShouldReturnEmptyList()
        {
            string inputValue = null;

            IList<string> separatedList = MsBuildStringUtilities.SplitByNewLine(inputValue);

            Assert.AreEqual(0, separatedList.Count,
                "Input value 'null' should return list with zero entries.");
        }

        [TestMethod]
        public void ReplaceNewLineWithSpace_WithStringWithoutNewLine_ShouldReturnStringAsIs()
        {
            string inputValue = "This is a test string";

            string outputValue = MsBuildStringUtilities.ReplaceNewLineWithSpace(inputValue);

            Assert.AreEqual(inputValue, outputValue,
                "A string without new line should not be altered.");
        }

        [TestMethod]
        public void
            ReplaceNewLineWithSpace_WithStringSeparatedByNewLineWithoutSpaces_ShouldReturnStringWithReplacedNewLines()
        {
            string inputValue = "This is a test string\nseparated by new line.";

            string outputValue = MsBuildStringUtilities.ReplaceNewLineWithSpace(inputValue);

            Assert.AreEqual("This is a test string separated by new line.", outputValue,
                "A string separated by new line should return a 1:1 replacement with a space");
        }

        [TestMethod]
        public void
            ReplaceNewLineWithSpace_WithStringSeparatedByNewLineFollowedByWhiteSpace_ShouldReturnStringWithReplacedNewLinesAndTrimmedWhiteSpace()
        {
            string inputValue = "This is a test string\n separated by new line.";

            string outputValue = MsBuildStringUtilities.ReplaceNewLineWithSpace(inputValue);

            Assert.AreEqual("This is a test string separated by new line.", outputValue,
                "A string separated by new line should return a 1:1 replacement with a space");
        }

        [TestMethod]
        public void
            ReplaceNewLineWithSpace_WithStringSeparatedByNewLineWithPredecessorAndFollowedByWhiteSpace_ShouldReturnStringWithReplacedNewLinesAndTrimmedWhiteSpace()
        {
            string inputValue = "This is a test string \n separated by new line.";

            string outputValue = MsBuildStringUtilities.ReplaceNewLineWithSpace(inputValue);

            Assert.AreEqual("This is a test string separated by new line.", outputValue,
                "A string separated by new line should return a 1:1 replacement with a space");
        }
    }
}