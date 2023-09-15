using BindOpen.Plus.Commands.Tests;
using NUnit.Framework;

namespace BindOpen.Plus.Commands
{
    [TestFixture, Order(400)]
    public class OptionsTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(4)]
        public void FromClassTest()
        {
            var optionSet = BdoCommands.NewOptionSet<OptionFake>();

            Assert.That(optionSet.Count == 3, "Bad argument parsing");
        }
    }
}
