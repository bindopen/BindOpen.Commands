using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Elements;
using BindOpen.Framework.MetaData.Specification;
using System;
using System.Linq;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option factory.
    /// </summary>
    public static class BdoOptions
    {
        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSpec CreateSpec(params string[] aliases)
            => CreateSpec(OptionNameKind.OnlyName, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSpec CreateSpec(
            OptionNameKind nameKind,
            params string[] aliases)
            => CreateSpec(DataValueTypes.Text, nameKind, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSpec CreateSpec(
            DataValueTypes valueType,
            OptionNameKind nameKind,
            params string[] aliases)
            => CreateSpec(valueType, RequirementLevels.Required, nameKind, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="requirementLevel">The requirement level of the entry to add.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSpec CreateSpec(
            RequirementLevels requirementLevel,
            params string[] aliases)
            => CreateSpec(
                DataValueTypes.Text,
                requirementLevel,
                aliases.Any(p => p?.Contains(StringHelper.__PatternEmptyValue) == true) ? OptionNameKind.NameWithValue : OptionNameKind.OnlyName,
                aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="requirementLevel">The requirement level of the entry to add.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSpec CreateSpec(
            RequirementLevels requirementLevel,
            OptionNameKind nameKind,
            params string[] aliases)
            => CreateSpec(DataValueTypes.Text, requirementLevel, nameKind, aliases);

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="requirementLevel">The requirement level of the entry to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSpec CreateSpec(
            DataValueTypes valueType,
            RequirementLevels requirementLevel,
            OptionNameKind nameKind,
            params string[] aliases)
        {
            OptionSpec spec = BdoElementSpecs.Create<OptionSpec>();

            spec.WithValueType(valueType);
            spec.WithAliases(aliases ?? new string[1] { "{{*}}" });
            spec.WithMinimumItemNumber(nameKind.HasValue() ? 1 : 0);
            spec.WithMaximumItemNumber(nameKind.HasName() ? 0 : 1);
            spec.WithRequirementLevel(requirementLevel);

            return spec;
        }

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        /// <param name="requirementLevel">The requirement level of the option to consider.</param>
        /// <param name="nameKind">The name kind to consider.</param>
        /// <param name="aliases">Aliases of the option to add.</param>
        public static OptionSpec CreateSpec(
            Type type,
            RequirementLevels requirementLevel,
            OptionNameKind nameKind,
            params string[] aliases)
        {
            var spec = CreateSpec(type.GetValueType(), requirementLevel, nameKind, aliases);

            if (type?.IsEnum == true)
            {
                var statement = new DataConstraintStatement();
                //statement.Add(
                //    "standard$" + KnownRoutineKinds.ItemMustBeInList,
                //    BdoElements.CreateSet(
                //        BdoElements.CreateScalar(DataValueTypes.Text, type.GetEnumFields())));
                spec.WithConstraintStatement(statement);
            }

            return spec;
        }
    }
}
