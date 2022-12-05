using BindOpen.Logging;
using NUnit.Framework;

namespace BindOpen.Tests.Logging
{
    [TestFixture, Order(400)]
    public class OptionSpecTests
    {
        private IBdoLog _log;

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = new
            {
                itemNumber = 1000
            };
        }

        [Test, Order(1)]
        public void CreateOptionsTest()
        {

        }
    }
}
