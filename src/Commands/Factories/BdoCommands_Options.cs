using BindOpen.System.Data;
using BindOpen.System.Data.Conditions;
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
            string name = null,
            params string[] aliases)
            => NewOption(LabelFormats.OnlyName, name, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            LabelFormats format,
            string name = null,
            params string[] aliases)
            => NewOption(LabelFormats.OnlyName, DataValueTypes.Any, name, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            LabelFormats format,
            DataValueTypes valueType,
            string name = null,
            params string[] aliases)
        {
            var spec = BdoData.NewSpec<Option>();

            spec
                .WithLabel(format)
                .WithAliases(aliases)
                .WithDataType(valueType)
                .WithMinDataItemNumber((uint)(format.HasValue() ? 1 : 0))
                .WithMaxDataItemNumber((uint)(format.HasName() ? 0 : 1))
                .WithName(name);

            return spec;
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            IBdoCondition condition,
            string name = null,
            params string[] aliases)
            => NewOption(condition, LabelFormats.OnlyName, name, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            IBdoCondition condition,
            LabelFormats format,
            string name = null,
            params string[] aliases)
        {
            var spec = BdoData.NewSpec<Option>();

            spec
                .WithCondition(condition)
                .WithLabel(format)
                .WithAliases(aliases)
                .WithMinDataItemNumber((uint)(format.HasValue() ? 1 : 0))
                .WithMaxDataItemNumber((uint)(format.HasName() ? 0 : 1))
                .WithName(name);

            return spec;
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSet NewOptionSet(
            params IOption[] options)
        {
            var set = new OptionSet();
            set.With(options);
            return set;
        }
    }
}
