using BindOpen.Commands.Options;

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
        public static OptionSet NewOptionSet(
            params IOption[] options)
        {
            var set = new OptionSet();
            set.With(options);
            return set;
        }
    }
}
