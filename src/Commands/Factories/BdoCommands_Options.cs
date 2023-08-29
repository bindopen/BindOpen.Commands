using BindOpen.System.Data;
using BindOpen.System.Data.Meta;

namespace BindOpen.Labs.Commands
{
    /// <summary>
    /// This class represents a option factory.
    /// </summary>
    public static partial class BdoCommands
    {
        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            params string[] aliases)
            => NewOption(LabelFormats.OnlyName, RequirementLevels.Optional, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            LabelFormats format,
            params string[] aliases)
            => NewOption(LabelFormats.OnlyName, DataValueTypes.Null, RequirementLevels.Optional, aliases);

        public static Option NewOption(
            DataValueTypes valueType,
            params string[] aliases)
            => NewOption(LabelFormats.NameSpaceValue, valueType, RequirementLevels.Optional, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            RequirementLevels requirementLevel,
            params string[] aliases)
            => NewOption(LabelFormats.OnlyName, requirementLevel, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            LabelFormats format,
            RequirementLevels requirementLevel,
            params string[] aliases)
            => NewOption(format, DataValueTypes.Null, requirementLevel, aliases);

        public static Option NewOption(
            DataValueTypes valueType,
            RequirementLevels requirementLevel,
            params string[] aliases)
            => NewOption(LabelFormats.NameSpaceValue, valueType, requirementLevel, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            LabelFormats format,
            DataValueTypes valueType,
            params string[] aliases)
            => NewOption(format, valueType, RequirementLevels.Optional, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            LabelFormats format,
            DataValueTypes valueType,
            RequirementLevels requirementLevel,
            params string[] aliases)
        {
            var spec = BdoData.NewSpec<Option>();

            spec
                .WithLabel(format)
                .WithAliases(aliases)
                .WithDataType(valueType)
                .WithRequirement(requirementLevel)
                .WithMinDataItemNumber((uint)(format.HasValue() ? 1 : 0))
                .WithMaxDataItemNumber((uint)(format.HasName() ? 0 : 1));

            return spec;
        }

        // Group

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewSectionOption(
            RequirementLevels requirementLevel,
            string name = null,
            params IOption[] children)
            => NewOption(LabelFormats.Any, DataValueTypes.Null, requirementLevel)
                .WithName(name)
                .WithChildren(children);

        public static Option NewSectionOption(
            string name,
            params IOption[] children)
            => NewSectionOption(RequirementLevels.Optional, name, children);

        public static Option NewSectionOption(
            params IOption[] children)
            => NewSectionOption(null as string, children);
    }
}
