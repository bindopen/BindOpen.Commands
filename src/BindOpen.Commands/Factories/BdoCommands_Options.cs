using BindOpen.Commands.Options;
using BindOpen.Data;
using BindOpen.Data.Meta;

namespace BindOpen.Commands
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
            => NewOption(OptionNameKind.OnlyName, name, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            OptionNameKind nameKind,
            string name = null,
            params string[] aliases)
        {
            var spec = BdoMeta.NewSpec<Option>();

            spec
                .WithAliases(aliases ?? new string[1] { "{{*}}" })
                .WithMinimumItemNumber((uint)(nameKind.HasValue() ? 1 : 0))
                .WithMaximumItemNumber((uint)(nameKind.HasName() ? 0 : 1))
                .WithName(name);

            return spec;
        }
    }
}
