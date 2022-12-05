namespace BindOpen.Commands
{
    /// <summary>
    /// The extension of the option name kind.
    /// </summary>
    public static class OptionNameKindExtension
    {
        /// <summary>
        /// Indicates whether the specified kind has name.
        /// </summary>
        /// <param name="kind">The kind of the option name.</param>
        /// <returns>Returns true or false.</returns>
        public static bool HasName(this OptionNameKind kind)
        {
            return kind == OptionNameKind.NameThenValue || kind == OptionNameKind.NameWithValue || kind == OptionNameKind.OnlyName;
        }

        /// <summary>
        /// Indicates whether the specified kind has value.
        /// </summary>
        /// <param name="kind">The kind of the option name.</param>
        /// <returns>Returns true or false.</returns>
        public static bool HasValue(this OptionNameKind kind)
        {
            return kind == OptionNameKind.NameThenValue || kind == OptionNameKind.NameWithValue || kind == OptionNameKind.OnlyValue;
        }
    }
}