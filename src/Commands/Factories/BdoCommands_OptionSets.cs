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
        public static OptionSet NewOptionSet(
            string name,
            params IOption[] options)
        {
            var set = BdoData.New<OptionSet>();
            set.WithName(name)
                .With(options);

            return set;
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSet NewOptionSet(
            params IOption[] options)
            => NewOptionSet(null, options);
    }
}
