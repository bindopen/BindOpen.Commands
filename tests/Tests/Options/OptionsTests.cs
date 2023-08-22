using BindOpen.Labs.Commands;
using BindOpen.Labs.Commands.Tests;
using NUnit.Framework;

namespace BindOpen.Tests.Commands
{
    [TestFixture, Order(400)]
    public class OptionsTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(3)]
        public void FromOptionSetTest()
        {
            var options = OptionSetFaker.CreateFlat();

            var args = new[] { "--version", "1.0", "-h", "-i 123" };

            var parameters = args.ParseArguments(options);
            Assert.That(parameters.Count == 3, "Bad argument parsing");
        }

        [Test, Order(4)]
        public void FromClassTest()
        {
            //var options = BdoCommands.NewOptionSet<OptionFake>();

            //Assert.That(options.Children().Count == 3, "Bad argument parsing");
        }
    }
}
