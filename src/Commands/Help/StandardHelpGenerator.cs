using BindOpen.System.Data;
using BindOpen.System.Data.Helpers;
using System.Linq;

namespace BindOpen.Labs.Commands
{
    /// <summary>
    /// This class represents a option specification set.
    /// </summary>
    public class StandardHelpGenerator : IHelpGenerator
    {
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

            help += "Description: ";
            help += "\r\n" + optionSet?.Description?[uiCulture];
            help += "\r\n";

            // Usage

            help += "\r\n";
            help += "\r\n" + "Usage: ";
            help += optionSet?.Name;

            foreach (var option in optionSet)
            {
                help += GetUsageDescription(option, uiCulture);
            }

            // Option description

            help += "\r\n";
            help += "\r\n" + "Options:";

            foreach (var option in optionSet)
            {
                help += GetOptionDescription(option, uiCulture, " ");
            }

            help += "\r\n";

            return help;
        }

        private string GetUsageDescription(IOption option, string uiCulture = StringHelper.__Star)
        {
            var help = "";

            if (option == null) return help;

            help += " ";

            var label = "";
            if (option.Label != null)
            {
                label = option.Name + " " + (option.Title?[uiCulture, StringHelper.__Star] ?? "value");

                //label = option.Label.Format(
                //    (LabelFormatsExtensions.__Script_This_Name, option.Name),
                //    (LabelFormatsExtensions.__Script_This_Value, option.Title[uiCulture, StringHelper.__Star]));
            }
            else
            {
                label = option.Name;
            }
            if (option._Children?.Any() == true)
            {
                foreach (var child in option._Children)
                {
                    label += GetUsageDescription(option, uiCulture);
                }
            }

            help += option.RequirementLevel switch
            {
                RequirementLevels.Required => "[" + label + "]",
                RequirementLevels.Optional => "(" + label + ")",
                _ => "[" + option.Name + "]",
            };

            return help;
        }

        private string GetOptionDescription(IOption option, string uiCulture = StringHelper.__Star, string offset = "")
        {
            var help = "";

            if (option == null) return help;

            help += "\r\n" + offset + option.Name + ": " + option.Description?[uiCulture, StringHelper.__Star];

            if (option._Children?.Any() == true)
            {
                foreach (var child in option._Children)
                {
                    help += "\r\n" + offset + GetOptionDescription(option, uiCulture, offset + " ");
                }
            }

            return help;
        }
    }
}
