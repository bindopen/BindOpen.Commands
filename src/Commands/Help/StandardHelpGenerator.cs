using BindOpen.System.Data;
using BindOpen.System.Data.Helpers;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Script;
using System.Linq;

namespace BindOpen.Labs.Commands
{
    /// <summary>
    /// This class represents a option specification set.
    /// </summary>
    public class StandardHelpGenerator : IHelpGenerator
    {
        public IBdoScope Scope { get; set; }

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the HelpGenerator class.
        /// </summary>
        public StandardHelpGenerator() : base()
        {
        }

        #endregion

        /// <summary>
        /// Gets the help text.
        /// </summary>
        /// <param name="uiCulture">The UI culture to consider.</param>
        /// <returns>Returns the help text.</returns>
        public string GetHelpText(IOptionSet optionSet, string uiCulture = StringHelper.__Star)
        {
            if (optionSet == null) return null;

            string help = "";

            // Description

            var description = optionSet?.Description?[uiCulture, StringHelper.__Star];
            if (!string.IsNullOrEmpty(description))
            {
                help += "Description: ";
                help += "\r\n" + description;
                help += "\r\n" + "\r\n";
            }

            // Usage

            help += "Usage: ";
            help += optionSet?.Name;

            var subHelp = "";
            foreach (var option in optionSet.OrderBy(q => q.Index ?? int.MaxValue))
            {
                var (h, sH) = GetUsageDescription(option, uiCulture);

                help += h;
                subHelp += sH;
            }
            help += "\r\n";

            if (!string.IsNullOrEmpty(subHelp))
            {
                help += subHelp;
                help += "\r\n";
            }
            help += "\r\n";

            // Option description

            var optionsLabel = "";
            foreach (var option in optionSet)
            {
                optionsLabel += GetOptionDescription(option, uiCulture, "  ");
            }
            if (!string.IsNullOrEmpty(optionsLabel))
            {
                help += "Options:";
                help += optionsLabel;
                help += "\r\n";
            }

            return help;
        }

        private (string Help, string SubHelp) GetUsageDescription(IOption option, string uiCulture = StringHelper.__Star, string subHelpOffset = "  ")
        {
            var help = "";
            var subHelp = "";

            if (option == null) return (help, subHelp);

            help = " ";
            subHelp = "";

            var label = GetOptionLabel(option, false, uiCulture);

            if (option._Children?.Any() == true)
            {
                foreach (var child in option._Children.OrderBy(q => q.Index ?? int.MaxValue))
                {
                    if (child is IOption optionChild)
                    {
                        var (h, sH) = GetUsageDescription(optionChild, uiCulture, subHelpOffset + "  ");

                        subHelp += h + sH;
                    }
                }
            }

            var varSet = BdoData.NewMetaSet((BdoScript.__VarName_This, option));
            var requirementLevel = option.RequirementStatement?.GetItem(Scope, varSet) ?? RequirementLevels.None;

            help += requirementLevel switch
            {
                RequirementLevels.Required => label,
                RequirementLevels.Optional => "[" + label + "]",
                _ => label,
            };

            if (!string.IsNullOrEmpty(subHelp))
            {
                var group = GetOptionLabel(option, true, uiCulture);
                subHelp = "\r\n" + subHelpOffset + group + ":" + subHelp;
            }


            return (help, subHelp);
        }

        private string GetOptionDescription(IOption option, string uiCulture = StringHelper.__Star, string offset = "")
        {
            var help = "";

            if (option == null) return help;

            var subHelp = "";
            if (option._Children?.Any() == true)
            {
                foreach (var child in option._Children.OrderBy(q => q.Index ?? int.MaxValue))
                {
                    if (child is IOption optionChild)
                    {
                        subHelp += GetOptionDescription(optionChild, uiCulture, offset + "  ");
                    }
                }
            }

            var description = option.Description?[uiCulture, StringHelper.__Star];

            if (!string.IsNullOrEmpty(description) || !string.IsNullOrEmpty(subHelp))
            {
                var label = GetOptionLabel(option, true, uiCulture);

                var valueTypeText = "";
                if (option._Children?.Any() != true
                    && option.DataType?.ValueType != null
                    && option.DataType?.ValueType != DataValueTypes.Any
                    && option.DataType?.ValueType != DataValueTypes.None
                    && option.DataType?.ValueType != DataValueTypes.Null
                    && option.DataType?.ValueType != DataValueTypes.Text)
                {
                    valueTypeText = "(" + option.DataType.ValueType.ToString().ToLower() + ") ";
                }

                help += "\r\n" + offset + label + ": " + valueTypeText + description;

                help += subHelp;
            }

            return help;
        }

        private string GetOptionLabel(IOption option, bool shortMode, string uiCulture = StringHelper.__Star)
        {
            if (option == null) return null;

            var label = "";

            if (shortMode && option._Children?.Any() == true)
            {
                label = option.Title?[uiCulture, StringHelper.__Star] ?? option.Name;
            }
            else
            {
                var labels = option.Aliases?.Select(q => q?.Trim()).ToList();
                if (shortMode)
                {
                    label = labels.FirstOrDefault();
                }
                else
                {
                    label = string.Join("|", labels);
                }

                if (option.Label != null)
                {
                    var optionLabel = option.Label;

                    var hasValue = optionLabel.ExtractTokens().Any(q => q.BdoKeyEquals(LabelFormatsExtensions.__This_Value));

                    var valueLabel = option.Title?[uiCulture, StringHelper.__Star] ?? (!string.IsNullOrEmpty(option.Name) ? option.Name : labels.FirstOrDefault());

                    if (shortMode && (!hasValue || option.DataType?.ValueType != DataValueTypes.Null))
                    {
                        label = valueLabel;
                    }
                    else
                    {
                        if (option._Children?.Any() == true)
                        {
                            if (option.DataType?.ValueType != DataValueTypes.Null || hasValue)
                            {
                                optionLabel = LabelFormats.NameSpaceValue.GetScript();
                                valueLabel = "[" + valueLabel + "]";
                            }
                            else
                            {
                                optionLabel = LabelFormats.OnlyValue.GetScript();
                            }
                        }
                        else
                        {
                            if (option.DataType?.ValueType != DataValueTypes.Null && !hasValue)
                            {
                                optionLabel = LabelFormats.OnlyValue.GetScript();
                            }

                            valueLabel = "<" + valueLabel + ">";
                        }

                        label = optionLabel.FormatFromTokens(
                            BdoData.NewMetaSet(
                                (LabelFormatsExtensions.__This_Name, label),
                                (LabelFormatsExtensions.__This_Value, valueLabel)));
                    }
                }
            }

            return label;
        }
    }
}
