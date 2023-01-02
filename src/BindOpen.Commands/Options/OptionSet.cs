using BindOpen.Data;
using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option specification set.
    /// </summary>
    public class OptionSet : TBdoItemSet<IOption>, IOptionSet
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

            foreach (var aElementSpec in Items)
            {
                foreach (string aAlias in aElementSpec.Aliases)
                    helpText += (helpText?.Length == 0 ? string.Empty : ", ") + aAlias;
                helpText += ": " + aElementSpec.Description?[uiCulture] + "\r\n";
            }

            return helpText;
        }

        #endregion

        // -------------------------------------------------------------
        // ITBdoItemSet Implementation
        // -------------------------------------------------------------

        #region ITBdoItemSet        

        /// <summary>
        /// Returns the item with the specified name.
        /// </summary>
        /// <param name="key">The name of the alias of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public new IOption Get(string key = null)
        {
            if (key == null) return this[0];

            return Items.Find(p =>
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
        // IGloballyDescribed Implementation
        // ------------------------------------------

        #region IGloballyDescribed

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Description { get; set; }

        public IOptionSet AddDescription(KeyValuePair<string, string> item)
        {
            Description ??= BdoItems.NewDictionary();
            Description.Add(item);
            return this;
        }

        public IOptionSet WithDescription(IBdoDictionary dictionary)
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
