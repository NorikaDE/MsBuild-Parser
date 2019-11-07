using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Converter;
using Norika.MsBuild.Model.Interfaces;
using Norika.MsBuild.Model.Types;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class StringToContinueOnErrorConverterUnitTest
    {
        private IMsBuildConverter<ContinueOnError> _sutParser;

        [TestInitialize]
        public void InitializeTest()
        {
            MsBuildConverterFactory factory = new MsBuildConverterFactory();
            _sutParser = factory.CreateConverter<ContinueOnError>();
        }
        
        [TestMethod]
        public void Parse_WithInputValueTrue_ShouldReturnWarnAndContinue()
        {
            Assert.AreEqual(ContinueOnError.WarnAndContinue, _sutParser.Parse("True"));
        }
        
        [TestMethod]
        public void Parse_WithInputValueFalse_ShouldReturnErrorAndStop()
        {
            Assert.AreEqual(ContinueOnError.ErrorAndStop, _sutParser.Parse("False"));
        }
        
        [TestMethod]
        public void Parse_WithInputValueFalseUpperCase_ShouldIgnoreCaseAndReturnErrorAndStop()
        {
            Assert.AreEqual(ContinueOnError.ErrorAndStop, _sutParser.Parse("FALSE"));
        }
        
        [TestMethod]
        public void Parse_WithInputWarnAndContinue_ShouldReturnEnumValueWarnAndContinue()
        {
            Assert.AreEqual(ContinueOnError.WarnAndContinue, _sutParser.Parse("WarnAndContinue"));
        }
        
        [TestMethod]
        public void Parse_WithInputValueErrorAndStop_ShouldReturnEnumValueErrorAndStop()
        {
            Assert.AreEqual(ContinueOnError.ErrorAndStop, _sutParser.Parse("ErrorAndStop"));
        }
        
        [TestMethod]
        public void Parse_WithInputValueErrorAndContinue_ShouldReturnEnumValueErrorAndContinue()
        {
            Assert.AreEqual(ContinueOnError.ErrorAndContinue, _sutParser.Parse("ErrorAndContinue"));
        }
        
        [TestMethod]
        public void Parse_WithInputValueErrorAndContinueUpperCase_ShouldIgnoreCaseAndReturnEnumValueErrorAndContinue()
        {
            Assert.AreEqual(ContinueOnError.ErrorAndContinue, _sutParser.Parse("ERRORANDCONTINUE"));
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Parse_WithUnknownValue_ShouldThrowArgumentOutOfRangeException()
        {
            _sutParser.Parse("UNKNOWN");
        }
    }
}