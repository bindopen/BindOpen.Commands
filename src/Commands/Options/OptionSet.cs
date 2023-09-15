using BindOpen.System.Data;
using BindOpen.System.Data.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Pulp.Commands
{
    /// <summary>
    /// This class represents a option specification set.
    /// </summary>
    public class OptionSet : TBdoSet<IOption>, IOptionSet
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSpecSet class.
        /// </summary>
        public OptionSet() : base()
        {
        }

        #endregion

        // -------------------------------------------------------------
        // IOptionSpecSet Implementation
        // -------------------------------------------------------------

        #region IOptionSpecSet

        /// <summary>
        /// Gets the help text.
        /// </summary>
        /// <param name="uiCulture">The UI culture to consider.</param>
        /// <returns>Returns the help text.</returns>
        public string GetHelpText(string uiCulture = "*")
        {
            string helpText = Description?[uiCulture];

            foreach (var option in this)
            {
                foreach (string aAlias in option.Aliases)
                    helpText += (helpText?.Length == 0 ? string.Empty : ", ") + aAlias;
                helpText += ": " + option.Description?[uiCulture] + "\r\n";
            }

            return helpText;
        }

        #endregion

        // -------------------------------------------------------------
        // ITBdoSet Implementation
        // -------------------------------------------------------------

        #region ITBdoSet        

        /// <summary>
        /// Returns the item with the specified name.
        /// </summary>
        /// <param name="key">The name of the alias of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public override IOption Get(string key)
        {
            if (Items == null)
                return default;

            return Items.FirstOrDefault(p =>
                    p.BdoKeyEquals(key) || p?.Aliases?.Any(q => q.BdoKeyEquals(key)) == true);
        }

        #endregion

        // -------------------------------------------------------------
        // IOptionSpecSet Implementation
        // -------------------------------------------------------------

        #region IOptionSpecSet

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IOptionSet WithName(string name)
        {
            Name = name;

            return this;
        }

        #endregion

        // ------------------------------------------
        // IBdoDescribed Implementation
        // ------------------------------------------

        #region IBdoDescribed

        /// <summary>
        /// 
        /// </summary>
        public ITBdoDictionary<string> Description { get; set; }

        public IOptionSet AddDescription(KeyValuePair<string, string> item)
        {
            Description ??= BdoData.NewDictionary<string>();
            Description.Add(item);
            return this;
        }

        public IOptionSet WithDescription(ITBdoDictionary<string> dictionary)
        {
            Description = dictionary;
            return this;
        }

        public string GetDescriptionText(string key = "*", string defaultKey = "*")
        {
            return Description?[key, defaultKey];
        }

        #endregion
    }
}
