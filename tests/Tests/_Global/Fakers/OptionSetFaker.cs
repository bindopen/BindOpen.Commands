using BindOpen.System.Data;
using BindOpen.System.Data.Conditions;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;

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
                    .AsRequired()
                    .WithDescription("Display the version of the application.")
                    .Execute(() => Task_Version())
                ,
                BdoCommands.NewOption("path")
                    .WithLabel(LabelFormats.OnlyValue)
                    .WithDataType(DataValueTypes.Text)
                    .AsRequired()
                    .WithTitle("_path")
                    .WithDescription("The path."),

                BdoCommands.NewOption("additional-deps")
                    .WithDataType(DataValueTypes.Integer)
                    .WithTitle("depth")
                    .WithDescription("The additional depth."),

                BdoCommands.NewOption("help", "--help", "-h")
                    .WithNullValue()
                ,
                BdoCommands.NewOption(LabelFormats.NameSpaceValue, DataValueTypes.Integer, "input", "--i", "-i")
                    .WithTitle("inputs")
                    .WithDescription("The inputs of the application.")
                    .WithIndex(1)
                    .WithChildren(
                        BdoCommands.NewOption("auto", "--a", "-auto")
                            .WithDescription("Indicates whether input is automatic."),
                        BdoCommands.NewOption(DataValueTypes.Text, "file", "--f", "-f")
                            .WithCondition((BdoCondition)BdoScript._Parent<IBdoMetaData>()._Has("auto"))
                            .AsRequired((BdoCondition)BdoScript._Parent<IBdoMetaData>()._Has("file"))
                            .Execute(() => Task_Inputs())
                ),
                BdoCommands.NewSectionOption(
                    "output",
                    BdoCommands.NewOption(LabelFormats.NameSpaceValue, "output", "--o", "-o"),
                    BdoCommands.NewOption("auto", "--a", "-auto")
                        .WithDescription("Indicates whether output is automatic."),
                    BdoCommands.NewOption(DataValueTypes.Text, "file", "--f", "-f"),
                    BdoCommands.NewSectionOption(
                        "sub.output",
                        BdoCommands.NewOption(LabelFormats.NameSpaceValue, "sub.output", "--so", "-so"),
                        BdoCommands.NewOption("sauto", "--a", "-sauto")
                            .WithDescription("Indicates whether output is automatic."),
                        BdoCommands.NewOption(DataValueTypes.Text, "sfile", "--sf", "-sf")))
                    .WithTitle("outputs")
                    .WithDescription("The outputs of the application.")
                    .WithIndex(2)
            )
            .WithDescription("Sample show you the way to simply specify the options of your application.");

        public static void Task_Version()
        {
        }

        public static void Task_Inputs()
        {
        }
    }
}