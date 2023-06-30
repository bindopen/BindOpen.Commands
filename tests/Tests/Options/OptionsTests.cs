using BindOpen.Labs.Commands;
using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
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

        [Test, Order(1)]
        public void CreateOptionsTest()
        {
            var options = BdoCommands.NewOptionSet(
                BdoCommands.NewOption("version", "--version", "-v")
                    .WithLabel(LabelFormats.OnlyName)
                    .WithDataType(DataValueTypes.Text)
                    .AsRequired(),
                BdoCommands.NewOption(LabelFormats.OnlyName, "help", "--help", "-h"));

            var args = new[] { "--version", "-h" };

            var parameters = args.ParseArguments(options);
        }

        [Test, Order(2)]
        public void CreateArguments()
        {
            var commandLine = @"version=1 -h ""toto"" p --help";

            var parameters = commandLine.GetArguments();
        }
    }
}
