using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using System.Linq;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents the application argument parser.
    /// </summary>
    public static class AppArgumentHelper
    {
        /// <summary>
        /// Retrieves the arguments from the specified argument string values.
        /// </summary>
        /// <param name="arguments">The argument string values to consider.</param>
        /// <param name="optionSet">The option specification set to consider.</param>
        /// <param name="allowMissingItems">Indicates whether the items can be missing.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the log of argument building.</returns>
        public static IParameterSet ParseArguments(
            this string[] arguments,
            IOptionSet optionSet = null,
            bool allowMissingItems = false,
            IBdoLog log = null)
        {
            IParameterSet parameterSet = new ParameterSet();

            int index = 0;
            if (arguments != null)
            {
                while (index < arguments.Length)
                {
                    string currentArgumentString = arguments[index];

                    if (currentArgumentString != null)
                    {
                        IParameter option = null;

                        Option argumentSpec = null;

                        int aliasIndex = -2;
                        if (optionSet != null)
                        {
                            argumentSpec = optionSet
                               .FirstOrDefault(p => p.IsArgumentMarching(currentArgumentString, out aliasIndex))
                               as Option;
                        }

                        if (optionSet == null || argumentSpec == null && allowMissingItems)
                        {
                            option = BdoMeta.NewScalar<object, Parameter>(currentArgumentString, DataValueTypes.Text);
                            option.WithData(arguments.GetAt(index));
                            parameterSet.Add(option);
                        }
                        else if (argumentSpec != null && optionSet != null)
                        {
                            if (argumentSpec.ValueType == DataValueTypes.Any)
                            {
                                argumentSpec.WithDataValueType(DataValueTypes.Text);
                            }
                            option = BdoMeta.NewScalar<object, Parameter>(
                                argumentSpec.Name, argumentSpec.ValueType, argumentSpec);

                            option.WithSpecs(argumentSpec);
                            if (argumentSpec.DataRequirementLevel.IsPossible())
                            {
                                if (argumentSpec.Name.Contains(StringHelper.__PatternEmptyValue))
                                {
                                    option.WithName(argumentSpec.Name.ToSubstring(0, argumentSpec.Name.Length - StringHelper.__PatternEmptyValue.Length - 2));

                                    int valueIndex = -1;
                                    if (aliasIndex == -1)
                                        valueIndex = argumentSpec.Name.IndexOf(StringHelper.__PatternEmptyValue);
                                    else if (aliasIndex > -1)
                                        valueIndex = argumentSpec.Aliases[aliasIndex].IndexOf(StringHelper.__PatternEmptyValue);

                                    option.WithData(valueIndex < 0 ? string.Empty : currentArgumentString.ToSubstring(valueIndex));
                                }
                                else
                                {
                                    index++;
                                    if (index < arguments.Length)
                                        option.WithData(arguments.GetAt(index));
                                }
                            }

                            parameterSet.Add(option);
                        }
                    }
                    index++;
                }
            }

            parameterSet.Check(optionSet, log: log);

            return parameterSet;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="optionSet">The set of options to consider.</param>
        /// <param name="optionSpecSet">The set of option specifications to consider.</param>
        /// <param name="allowMissingItems">Indicates whether the items can be missing.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the log of check.</returns>
        public static bool Check(
            this IParameterSet optionSet,
            IOptionSet optionSpecSet,
            bool allowMissingItems = false,
            IBdoLog log = null)
        {
            bool isValid = false;

            if (optionSet != null && optionSpecSet != null)
            {
                if (!allowMissingItems)
                {
                    foreach (var optionSpecification in optionSpecSet.Items.Where(p => p.RequirementLevel == RequirementLevels.Required))
                    {
                        if (!optionSet.Has(optionSpecification.Name))
                        {
                            isValid = false;
                            log.AddError("Option '" + optionSpecification.Name + "' missing");
                        }
                    }
                }

                foreach (var option in optionSet)
                {
                    var spec = option?.Specs?.Get();
                    if (spec != null)
                    {
                        var value = option.GetData<string>();

                        switch (spec.DataRequirementLevel)
                        {
                            case RequirementLevels.Required:
                                if (string.IsNullOrEmpty(value))
                                {
                                    isValid = false;
                                    log.AddError("Option '" + option.Name + "' requires value");
                                }
                                break;
                            case RequirementLevels.Forbidden:
                                if (!string.IsNullOrEmpty(value))
                                {
                                    isValid = false;
                                    log.AddError("Option '" + option.Name + "' does not allow value");
                                }
                                break;
                        }
                    }
                }
            }

            return isValid;
        }
    }
}