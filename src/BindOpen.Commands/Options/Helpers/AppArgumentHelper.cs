using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Elements;
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

                        Option argumentSpecification = null;

                        int aliasIndex = -2;
                        if (optionSet != null)
                        {
                            argumentSpecification = optionSet.Items
                               .Find(p => p.IsArgumentMarching(currentArgumentString, out aliasIndex))
                               as Option;
                        }

                        if (optionSet == null || argumentSpecification == null && allowMissingItems)
                        {
                            option = BdoElements.NewScalar<Parameter>(currentArgumentString, DataValueTypes.Text);
                            option.WithItem(arguments.GetAt(index));
                            parameterSet.Add(option);
                        }
                        else if (argumentSpecification != null && optionSet != null)
                        {
                            if (argumentSpecification.ValueType == DataValueTypes.Any)
                            {
                                argumentSpecification.WithValueType(DataValueTypes.Text);
                            }
                            option = BdoElements.NewScalar<Parameter>(
                                argumentSpecification.Name, argumentSpecification.ValueType, argumentSpecification);

                            option.WithSpecifications(argumentSpecification);
                            if (argumentSpecification.ItemRequirementLevel.IsPossible())
                            {
                                if (argumentSpecification.Name.Contains(StringHelper.__PatternEmptyValue))
                                {
                                    option.WithName(argumentSpecification.Name.ToSubstring(0, argumentSpecification.Name.Length - StringHelper.__PatternEmptyValue.Length - 2));

                                    int valueIndex = -1;
                                    if (aliasIndex == -1)
                                        valueIndex = argumentSpecification.Name.IndexOf(StringHelper.__PatternEmptyValue);
                                    else if (aliasIndex > -1)
                                        valueIndex = argumentSpecification.Aliases[aliasIndex].IndexOf(StringHelper.__PatternEmptyValue);

                                    option.WithItem(valueIndex < 0 ? string.Empty : currentArgumentString.ToSubstring(valueIndex));
                                }
                                else
                                {
                                    index++;
                                    if (index < arguments.Length)
                                        option.WithItem(arguments.GetAt(index));
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
        /// <param name="optionSpecificationSet">The set of option specifications to consider.</param>
        /// <param name="allowMissingItems">Indicates whether the items can be missing.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the log of check.</returns>
        public static bool Check(
            this IParameterSet optionSet,
            IOptionSet optionSpecificationSet,
            bool allowMissingItems = false,
            IBdoLog log = null)
        {
            bool isValid = false;

            if (optionSet?.Items != null && optionSpecificationSet != null)
            {
                if (!allowMissingItems)
                {
                    foreach (var optionSpecification in optionSpecificationSet.Items.Where(p => p.RequirementLevel == RequirementLevels.Required))
                    {
                        if (!optionSet.HasItem(optionSpecification.Name))
                        {
                            isValid = false;
                            log.AddError("Option '" + optionSpecification.Name + "' missing");
                        }
                    }
                }

                foreach (var option in optionSet.Items)
                {
                    var spec = option?.GetSpecification();
                    if (spec != null)
                    {
                        var value = option.GetItem<string>();

                        switch (spec.ItemRequirementLevel)
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