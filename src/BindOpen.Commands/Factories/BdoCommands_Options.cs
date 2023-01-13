using BindOpen.Commands.Options;
using BindOpen.MetaData;
using BindOpen.MetaData.Specification;
using System.Linq;

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
            string name,
            params string[] aliases)
            => NewOption(OptionNameKind.OnlyName, name, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            OptionNameKind nameKind,
            string name,
            params string[] aliases)
            => NewOption(DataValueTypes.Text, nameKind, name, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            DataValueTypes valueType,
            OptionNameKind nameKind,
            string name,
            params string[] aliases)
            => NewOption(
                valueType,
                RequirementLevels.Optional,
                nameKind,
                name,
                aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="requirementLevel">The requirement level of the entry to add.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            RequirementLevels requirementLevel,
            string name,
            params string[] aliases)
            => NewOption(
                DataValueTypes.Text,
                requirementLevel,
                aliases.Any(p => p?.Contains(StringHelper.__PatternEmptyValue) == true) ? OptionNameKind.NameWithValue : OptionNameKind.OnlyName,
                name,
                aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="requirementLevel">The requirement level of the entry to add.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            RequirementLevels requirementLevel,
            OptionNameKind nameKind,
            string name,
            params string[] aliases)
            => NewOption(
                DataValueTypes.Text,
                requirementLevel,
                nameKind,
                name,
                aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="requirementLevel">The requirement level of the entry to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption(
            DataValueTypes valueType,
            RequirementLevels requirementLevel,
            OptionNameKind nameKind,
            string name,
            params string[] aliases)
        {
            var spec = BdoMeta.NewSpec<Option>();

            spec
                .WithAliases(aliases ?? new string[1] { "{{*}}" })
                .WithMinimumItemNumber(nameKind.HasValue() ? 1 : 0)
                .WithMaximumItemNumber(nameKind.HasName() ? 0 : 1)
                .WithName(name)
                .WithRequirementLevel(requirementLevel)
                .WithValueType(valueType);

            return spec;
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        /// <param name="requirementLevel">The requirement level of the option to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static Option NewOption<T>(
            RequirementLevels requirementLevel,
            OptionNameKind nameKind,
            string name,
            params string[] aliases)
        {
            var spec = NewOption(
                typeof(T).GetValueType(),
                requirementLevel,
                nameKind,
                name,
                aliases);

            if (typeof(T)?.IsEnum == true)
            {
                var statement = new DataConstraintStatement();
                //statement.Add(
                //    "standard$" + KnownRoutineKinds.ItemMustBeInList,
                //    BdoMeta.CreateSet(
                //        BdoMeta.CreateScalar(DataValueTypes.Text, type.GetEnumFields())));
                spec.WithConstraintStatement(statement);
            }

            return spec;
        }
    }
}
