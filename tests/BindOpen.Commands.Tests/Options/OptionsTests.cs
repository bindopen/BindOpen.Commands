using BindOpen.Commands;
using BindOpen.Commands.Options;
using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using NUnit.Framework;

namespace BindOpen.Tests.Commands
{
    [TestFixture, Order(400)]
    public class OptionsTests
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
            var options = BdoCommands.NewOptionSet(
                BdoCommands.NewOption("version", "--version", "-v")
                    .WithDataValueType(DataValueTypes.Text)
                    .AsRequired(),
                BdoCommands.NewOption("help", "--help", "-h"));

            var args = new[] { "--version", "-h" };

            var parameters = args.ParseArguments(options);
        }
    }
}
