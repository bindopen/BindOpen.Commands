using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using System.Linq;

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

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSet NewOptionSet<T>(
            string name)
        {
            var options = typeof(T).ToSpec<Option>();

            var set = NewOptionSet(name, options?._Children?.Cast<IOption>().ToArray());

            return set;
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSet NewOptionSet<T>()
            => NewOptionSet<T>(null);
    }
}
