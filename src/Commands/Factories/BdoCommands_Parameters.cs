using BindOpen.System.Data;
using BindOpen.System.Logging;
using System.Linq;

namespace BindOpen.Labs.Commands
{
    /// <summary>
    /// This class represents the application argument parser.
    /// </summary>
    public static partial class BdoCommands
    {
        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="paramSet">The set of options to consider.</param>
        /// <param name="optionSet">The set of option specifications to consider.</param>
        /// <param name="allowMissingItems">Indicates whether the items can be missing.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the log of check.</returns>
        public static bool Check(
            this IParameterSet paramSet,
            IOptionSet optionSet,
            bool allowMissingItems = false,
            IBdoLog log = null)
        {
            bool isValid = false;

            if (paramSet != null && optionSet != null)
            {
                if (!allowMissingItems)
                {
                    foreach (var option in optionSet.Items.Where(p => p.Requirement == RequirementLevels.Required))
                    {
                        if (!paramSet.Has(option.Name))
                        {
                            isValid = false;
                            log.AddEvent(EventKinds.Error, "Option '" + option.Name + "' missing");
                        }
                    }
                }

                foreach (var param in paramSet)
                {
                    var option = param?.Specs?.Get();
                    if (option != null)
                    {
                        var value = param.GetData<string>();

                        switch (option.DataRequirement)
                        {
                            case RequirementLevels.Required:
                                if (string.IsNullOrEmpty(value))
                                {
                                    isValid = false;
                                    log.AddEvent(EventKinds.Error, "Option '" + option.Name + "' requires value");
                                }
                                break;
                            case RequirementLevels.Forbidden:
                                if (!string.IsNullOrEmpty(value))
                                {
                                    isValid = false;
                                    log.AddEvent(EventKinds.Error, "Option '" + option.Name + "' does not allow value");
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