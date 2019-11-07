using Microsoft.VisualStudio.TestTools.UnitTesting;
using Norika.MsBuild.Core.Data.Converter;
using Norika.MsBuild.Model.Interfaces;
using Norika.MsBuild.Model.Types;

namespace Norika.MsBuild.Data.UnitTests
{
    [TestClass]
    public class MsBuildConverterFactoryUnitTest
    {
        [TestMethod]
        public void CreateConverter_ForContinueOnErrorEnum_ShouldReturnObjectOfTypeMsBuildStringToContinueOnErrorConverter()
        {
            MsBuildConverterFactory factory = new MsBuildConverterFactory();

            IMsBuildConverter<ContinueOnError> converter = factory.CreateConverter<ContinueOnError>();
            
            Assert.IsInstanceOfType(converter, typeof(MsBuildStringToContinueOnErrorConverter));
        }

        [TestMethod]
        public void CreateConverter_ForTypeWithoutConverter_ShouldReturnNull()
        {
            MsBuildConverterFactory factory = new MsBuildConverterFactory();

            IMsBuildConverter<bool> converter = factory.CreateConverter<bool>();

            Assert.IsNull(converter);
        }
    }
}