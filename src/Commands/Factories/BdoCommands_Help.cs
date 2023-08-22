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
        public static string GetHelpText<T>(
            this IOptionSet optionSet)
            where T : IHelpGenerator, new()
        {
            var generator = new T();
            return generator.GetHelpText(optionSet);
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static string GetHelpText(this IOptionSet optionSet)
            => optionSet.GetHelpText<StandardHelpGenerator>();
    }
}
