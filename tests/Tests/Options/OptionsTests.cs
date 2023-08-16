using BindOpen.Labs.Commands;
using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using NUnit.Framework;
using System.Linq;

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
        public void CreateArguments()
        {
            string commandLine1 = null;
            var parameters1 = commandLine1.GetArguments();
            Assert.That(parameters1.Count() == 0, "Bad argument creation");


            var commandLine2 = "";
            var parameters2 = commandLine2.GetArguments();
            Assert.That(parameters2.Count() == 0, "Bad argument creation");

            var commandLine3 = @"version=1 -h ""toto"" p  'titi' --help";
            var parameters3 = commandLine3.GetArguments();

            Assert.That(parameters3.Count() == 6
                && parameters3[0] == "version=1"
                && parameters3[1] == "-h"
                && parameters3[2] == "\"toto\""
                && parameters3[3] == "p"
                && parameters3[4] == "'titi'"
                && parameters3[5] == "--help", "Bad argument creation");
        }

        [Test, Order(3)]
        public void ParseParametersTest()
        {
            var options = BdoCommands.NewOptionSet(
                BdoCommands.NewOption("version", "--version", "-v")
                    .WithLabel(LabelFormats.NameSpaceValue)
                    .WithDataType(DataValueTypes.Text)
                    .AsRequired(),
                BdoCommands.NewOption(LabelFormats.OnlyName, "help", "--help", "-h")
                    .WithDataType(DataValueTypes.Boolean),
                BdoCommands.NewOption(LabelFormats.NameSpaceValue, "input", "--i", "-i")
                    .WithDataType(DataValueTypes.Integer));

            var args = new[] { "--version", "1.0", "-h", "-i 123" };

            var parameters = args.ParseArguments(options);
            Assert.That(parameters.Count == 3, "Bad argument parsing");
        }

        [Test, Order(4)]
        public void ParseParametersFromObjectTest()
        {
            //var options = BdoCommands.NewOptionSet(
            //    BdoCommands.NewOption("version", "--version", "-v")
            //        .WithLabel(LabelFormats.NameSpaceValue)
            //        .WithDataType(DataValueTypes.Text)
            //        .AsRequired(),
            //    BdoCommands.NewOption(LabelFormats.OnlyName, "help", "--help", "-h")
            //        .WithDataType(DataValueTypes.Boolean),
            //    BdoCommands.NewOption(LabelFormats.NameSpaceValue, "input", "--i", "-i")
            //        .WithDataType(DataValueTypes.Integer));

            var args = new[] { "--version", "1.0", "-h", "-i 123" };
            //var parameters = args.ParseArguments<OptionFake>(options);

            //var optionFake = new OptionFake();
            //var metaSet = BdoData.NewMetaSet(parameters?.ToArray());
            //optionFake.UpdateFromMeta(metaSet);
        }

        [Test, Order(5)]
        public void GetOptionTextTest()
        {
            var options = BdoCommands.NewOptionSet(
                BdoCommands.NewOption("version", "--version", "-v")
                    .WithLabel(LabelFormats.NameSpaceValue)
                    .WithDataType(DataValueTypes.Text)
                    .AsRequired(),
                BdoCommands.NewOption(LabelFormats.OnlyName, "help", "--help", "-h")
                    .WithDataType(DataValueTypes.Boolean),
                BdoCommands.NewOption(LabelFormats.NameSpaceValue, "input", "--i", "-i")
                    .WithDataType(DataValueTypes.Integer));

            var help = options.GetHelpText();
            Assert.That(!string.IsNullOrEmpty(help), "Could not generate help text");
        }

        [Test, Order(6)]
        public void CascadeOptionTest()
        {
            //var options = BdoCommands.NewOptionSet(
            //    BdoCommands.NewOption("version", "--version", "-v")
            //        .WithLabel(LabelFormats.NameSpaceValue)
            //        .WithDataType(DataValueTypes.Text)
            //        .AsRequired(),
            //    BdoCommands.NewOption(LabelFormats.OnlyName, "help", "--help", "-h")
            //        .WithDataType(DataValueTypes.Boolean),
            //    BdoCommands.NewOption(LabelFormats.NameSpaceValue, "input", "--i", "-i")
            //        .WithDataType(DataValueTypes.Integer));

            //var help = options.GetHelpText();
            //Assert.That(!string.IsNullOrEmpty(help), "Bad cascade options");
        }
    }
}
