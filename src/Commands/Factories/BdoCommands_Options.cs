using BindOpen.Data;
using BindOpen.Data.Schema;

namespace BindOpen.Commands;

/// <summary>
/// This class represents a option factory.
/// </summary>
public static partial class BdoCommands
{
    /// <summary>
    /// Instantiates a new instance of the OptionSpec class.
    /// </summary>
    /// <param name="aliases">Aliases of the option to add.</param>
    public static IOption NewOption(
        string name = null)
        => NewOption(LabelFormats.OnlyName, RequirementLevels.Optional, name);

    /// <summary>
    /// Instantiates a new instance of the OptionSpec class.
    /// </summary>
    /// <param name="nameKind">The name kind to consider.</param>
    /// <param name="aliases">Aliases of the option to add.</param>
    public static IOption NewOption(
        LabelFormats format,
        string name = null)
        => NewOption(format, DataValueTypes.Any, RequirementLevels.Optional, name);

    public static IOption NewOption(
        DataValueTypes valueType,
        string name = null)
        => NewOption(LabelFormats.NameSpaceValue, valueType, RequirementLevels.Optional, name);

    /// <summary>
    /// Instantiates a new instance of the OptionSpec class.
    /// </summary>
    /// <param name="aliases">Aliases of the option to add.</param>
    public static IOption NewOption(
        RequirementLevels requirementLevel,
        string name = null)
        => NewOption(LabelFormats.OnlyName, requirementLevel, name);

    /// <summary>
    /// Instantiates a new instance of the OptionSpec class.
    /// </summary>
    /// <param name="nameKind">The name kind to consider.</param>
    /// <param name="aliases">Aliases of the option to add.</param>
    public static IOption NewOption(
        LabelFormats format,
        RequirementLevels requirementLevel,
        string name = null)
        => NewOption(format, DataValueTypes.Any, requirementLevel, name);

    public static IOption NewOption(
        DataValueTypes valueType,
        RequirementLevels requirementLevel,
        string name = null)
        => NewOption(LabelFormats.NameSpaceValue, valueType, requirementLevel, name);

    /// <summary>
    /// Instantiates a new instance of the OptionSpec class.
    /// </summary>
    /// <param name="nameKind">The name kind to consider.</param>
    /// <param name="aliases">Aliases of the option to add.</param>
    public static IOption NewOption(
        LabelFormats format,
        DataValueTypes valueType,
        string name = null)
        => NewOption(format, valueType, RequirementLevels.Optional, name);

    /// <summary>
    /// Instantiates a new instance of the OptionSpec class.
    /// </summary>
    /// <param name="nameKind">The name kind to consider.</param>
    /// <param name="aliases">Aliases of the option to add.</param>
    public static IOption NewOption(
        LabelFormats format,
        DataValueTypes valueType,
        RequirementLevels requirementLevel,
        string name = null)
    {
        var option = BdoData.NewSchema<Option>();

        option
            .WithLabel(format)
            .WithName(name)
            .WithDataType(valueType)
            .AddRequirement(requirementLevel);

        return option;
    }

    // Section

    /// <summary>
    /// Instantiates a new instance of the OptionSpec class.
    /// </summary>
    /// <param name="aliases">Aliases of the option to add.</param>
    public static IOption NewOption(
        RequirementLevels requirementLevel,
        string name = null,
        params IOption[] children)
        => NewOption(LabelFormats.Any, DataValueTypes.Any, requirementLevel)
            .WithName(name)
            .WithChildren(children);

    public static IOption NewOption(
        string name,
        params IOption[] children)
        => NewOption(RequirementLevels.Optional, name, children);

    public static IOption NewOption(
        params IOption[] children)
        => NewOption(null as string, children);

    // From

    /// <summary>
    /// Instantiates a new instance of the OptionSpec class.
    /// </summary>
    /// <param name="aliases">Aliases of the option to add.</param>
    public static TOption NewOptionFrom<TType, TOption>(string name = null)
        where TOption : IOption, new()
    {
        var options = BdoData.NewSchemaFrom<TType, TOption>(name);

        return options;
    }

    /// <summary>
    /// Instantiates a new instance of the OptionSpec class.
    /// </summary>
    /// <param name="aliases">Aliases of the option to add.</param>
    public static IOption NewOptionFrom<TType>()
        => NewOptionFrom<TType, Option>(null);
}
