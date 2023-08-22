using BindOpen.Labs.Commands;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Tests.Commands
{
    [TestFixture, Order(400)]
    public class ArgumentsTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void NullTest()
        {
            string commandLine1 = null;
            var parameters1 = commandLine1.GetArguments();
            Assert.That(parameters1.Count() == 0, "Bad argument creation");
        }

        [Test, Order(2)]
        public void EmptyTest()
        {
            var commandLine2 = "";
            var parameters2 = commandLine2.GetArguments();
            Assert.That(parameters2.Count() == 0, "Bad argument creation");
        }

        [Test, Order(2)]
        public void StandardTest()
        {
            var commandLine3 = @"version=1 -h ""toto"" p  'titi' --help";
            var parameters3 = commandLine3.GetArguments();

            Assert.That(
                parameters3.Count() == 6
                && parameters3[0] == "version=1"
                && parameters3[1] == "-h"
                && parameters3[2] == "\"toto\""
                && parameters3[3] == "p"
                && parameters3[4] == "'titi'"
                && parameters3[5] == "--help", "Bad argument creation");
        }
    }
}
