using BindOpen.System.Data;
using BindOpen.System.Data.Meta;

namespace BindOpen.Labs.Commands.Tests
{
    /// <summary>
    /// This class represents a fake class.
    /// </summary>
    public static class OptionSetFaker
    {
        public static IOptionSet CreateFlat() =>
             BdoCommands.NewOptionSet(
                 "sample",
                 BdoCommands.NewOption("version", "--version", "-v")
                     .WithLabel(LabelFormats.NameSpaceValue)
                     .WithDataType(DataValueTypes.Text)
                     .AsRequired(),
                 BdoCommands.NewOption(LabelFormats.OnlyName, "help", "--help", "-h")
                     .WithDataType(DataValueTypes.Boolean),
                 BdoCommands.NewOption(LabelFormats.NameSpaceValue, DataValueTypes.Integer, "input", "--i", "-i")
             );
    }
}