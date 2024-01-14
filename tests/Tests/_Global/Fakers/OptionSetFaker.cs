using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping.Script;

namespace BindOpen.Plus.Commands.Tests
{
    /// <summary>
    /// This class represents a fake class.
    /// </summary>
    public static class OptionSetFaker
    {
        public static IOption CreateFlat() =>
            BdoCommands.NewOption(
                "sample",
                BdoCommands.NewOption("version")
                    .WithAliases("version", "--version", "-v")
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

                BdoCommands.NewOption("help")
                    .WithAliases("help", "--help", "-h")
                    .AsNullValue()
                ,
                BdoCommands.NewOption(LabelFormats.NameSpaceValue, DataValueTypes.Integer, "input")
                    .WithAliases("input", "--i", "-i")
                    .WithTitle("inputs")
                    .WithDescription("The inputs of the application.")
                    .WithIndex(1)
                    .WithChildren(
                        BdoCommands.NewOption("auto")
                            .WithAliases("--a", "-auto")
                            .WithDescription("Indicates whether input is automatic."),
                        BdoCommands.NewOption(DataValueTypes.Text, "file")
                            .WithAliases("file", "--f", "-f")
                            .WithCondition(BdoScript._Parent<IBdoMetaData>()._Has("auto"))
                            .AsForbidden(BdoScript._Parent<IBdoMetaData>()._Has("auto").ToCondition())
                            .Execute(() => Task_Inputs())
                ),
                BdoCommands.NewOption(
                    "output",
                    BdoCommands.NewOption(LabelFormats.NameSpaceValue, "output")
                        .WithAliases("output", "--o", "-o"),
                    BdoCommands.NewOption("auto")
                        .WithAliases("auto", "--a", "-auto")
                        .WithDescription("Indicates whether output is automatic."),
                    BdoCommands.NewOption(DataValueTypes.Text, "file")
                        .WithAliases("file", "--f", "-f"),
                    BdoCommands.NewOption(
                        "sub.output",
                        BdoCommands.NewOption(LabelFormats.NameSpaceValue, "sub.output")
                            .WithAliases("sub.output", "--so", "-so"),
                        BdoCommands.NewOption("sauto")
                            .WithAliases("sauto", "--a", "-sauto")
                            .WithDescription("Indicates whether output is automatic."),
                        BdoCommands.NewOption(DataValueTypes.Text, "sfile")
                            .WithAliases("sfile", "--sf", "-sf")
                    )
                )
                    .WithTitle("outputs")
                    .WithDescription("The outputs of the application.")
                    .WithIndex(2)
            )
            .WithDescription("Sample shows you the way to simply specify the options of your application.");

        public static void Task_Version()
        {
        }

        public static void Task_Inputs()
        {
        }
    }
}