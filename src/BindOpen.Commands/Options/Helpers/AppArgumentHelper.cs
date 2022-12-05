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
        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Arguments --------------------------------

        /// <summary>
        /// Retrieves the arguments from the specified argument string values.
        /// </summary>
        /// <param name="arguments">The argument string values to consider.</param>
        /// <param name="optionSpecificationSet">The option specification set to consider.</param>
        /// <param name="allowMissingItems">Indicates whether the items can be missing.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the log of argument building.</returns>
        public static IOptionSet UpdateOptions(
            this string[] arguments,
            IOptionSpecSet optionSpecificationSet,
            bool allowMissingItems = false,
            IBdoLog log = null)
        {
            IOptionSet optionSet = new OptionSet();

            int index = 0;
            if (arguments != null)
            {
                while (index < arguments.Length)
                {
                    string currentArgumentString = arguments[index];

                    if (currentArgumentString != null)
                    {
                        IOption option = null;

                        OptionSpec argumentSpecification = null;

                        int aliasIndex = -2;
                        if (optionSpecificationSet != null)
                        {
                            argumentSpecification = optionSpecificationSet.Items
                               .Find(p => p.IsArgumentMarching(currentArgumentString, out aliasIndex))
                               as OptionSpec;
                        }

                        if (optionSpecificationSet == null || argumentSpecification == null && allowMissingItems)
                        {
                            option = BdoElements.CreateScalar<Option>(currentArgumentString, DataValueTypes.Text);
                            option.WithItem(arguments.GetAt(index));
                            optionSet.Add(option);
                        }
                        else if (argumentSpecification != null && optionSpecificationSet != null)
                        {
                            if (argumentSpecification.ValueType == DataValueTypes.Any)
                            {
                                argumentSpecification.WithValueType(DataValueTypes.Text);
                            }
                            option = BdoElements.CreateScalar<Option>(argumentSpecification.Name, null, argumentSpecification.ValueType, argumentSpecification);

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

                            optionSet.Add(option);
                        }
                    }
                    index++;
                }
            }

            optionSet.Check(optionSpecificationSet, log: log);

            return optionSet;
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
            this IOptionSet optionSet,
            IOptionSpecSet optionSpecificationSet,
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

        #endregion
    }
}