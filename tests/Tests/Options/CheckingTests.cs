using BindOpen.Plus.Commands.Tests;
using NUnit.Framework;

namespace BindOpen.Plus.Commands
{
    [TestFixture, Order(400)]
    public class CheckingTests
    {
        private IOption _option;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _option = OptionSetFaker.CreateFlat();
        }

        [Test, Order(1)]
        public void CheckArgumentsWithoutCheckTest()
        {
            var args = new[] { "--version", "1.0", "-h", "-i 123" };

            var parameters = SystemData.Scope.ParseArguments(args, _option, false);
            Assert.That(parameters?.Count == 3, "Bad argument parsing");

        }

        [Test, Order(2)]
        public void CheckArgumentsTest()
        {
            var args = new[] { "--version", "1.0", "-h", "-i 123" };

            var log = SystemData.CreateLog();

            var parameters = SystemData.Scope.ParseArguments(args, _option, log: log);
            Assert.That(parameters?.Count == 3, "Bad argument parsing");
        }
    }
}
