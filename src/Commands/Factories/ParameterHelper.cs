using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Script;
using System.Linq;

namespace BindOpen.Plus.Commands
{
    /// <summary>
    /// This class represents the application argument parser.
    /// </summary>
    public static partial class ParameterHelper
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
            this IBdoScope scope,
            IParameterSet paramSet,
            IOptionSet optionSet,
            bool allowMissingItems = false,
            IBdoLog log = null)
        {
            bool isValid = false;

            if (paramSet != null && optionSet != null)
            {
                if (!allowMissingItems)
                {
                    var options = optionSet.Items.Where(q =>
                    {
                        var varSet = BdoData.NewMetaSet((BdoScript.__VarName_This, q));
                        var requirementLevel = q.RequirementStatement?.GetItem(scope, varSet, log) ?? RequirementLevels.None;
                        return requirementLevel == RequirementLevels.Required;
                    });

                    foreach (var option in options)
                    {
                        if (!paramSet.Has(option.Name))
                        {
                            isValid = false;
                            log?.AddEvent(EventKinds.Error, "Option '" + option.Name + "' missing");
                        }
                    }
                }

                foreach (var param in paramSet)
                {
                    var option = param?.Spec;
                    if (option != null)
                    {
                        var varSet = BdoData.NewMetaSet((BdoScript.__VarName_This, param));
                        var requirementLevel = param.WhatItemRequirement(scope, varSet, log);

                        var value = param.GetData<string>();

                        switch (requirementLevel)
                        {
                            case RequirementLevels.Required:
                                if (string.IsNullOrEmpty(value))
                                {
                                    isValid = false;
                                    log?.AddEvent(EventKinds.Error, "Option '" + option.Name + "' requires value");
                                }
                                break;
                            case RequirementLevels.Forbidden:
                                if (!string.IsNullOrEmpty(value))
                                {
                                    isValid = false;
                                    log?.AddEvent(EventKinds.Error, "Option '" + option.Name + "' does not allow value");
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