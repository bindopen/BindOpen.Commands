using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Items;
using System.Linq;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option specification set.
    /// </summary>
    public class OptionSet : TDataItemSet<IOption>, IOptionSet
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
            string helpText = Description.GetText(uiCulture);

            foreach (var aElementSpec in Items)
            {
                foreach (string aAlias in aElementSpec.Aliases)
                    helpText += (helpText?.Length == 0 ? string.Empty : ", ") + aAlias;
                helpText += ": " + aElementSpec.Description.GetText(uiCulture) + "\r\n";
            }

            return helpText;
        }

        #endregion

        // -------------------------------------------------------------
        // ITDataItemSet Implementation
        // -------------------------------------------------------------

        #region ITDataItemSet        

        /// <summary>
        /// Returns the item with the specified name.
        /// </summary>
        /// <param name="key">The name of the alias of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public new IOption Get(string key = null)
        {
            if (key == null) return this[0];

            return Items.Find(p =>
                p.KeyEquals(key) || p?.Aliases?.Any(q => q.KeyEquals(key)) == true);
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
        public IDictionaryDataItem Description { get; set; }

        public IOptionSet AddDescription(IDataKeyValue item)
        {
            Description ??= BdoItems.NewDictionary();
            Description.Add(item);
            return this;
        }

        public IOptionSet WithDescription(IDictionaryDataItem dictionary)
        {
            Description = dictionary;
            return this;
        }

        public string GetDescriptionText(string key = "*", string defaultKey = "*")
        {
            string label = Description?.GetText(key);
            if (string.IsNullOrEmpty(label))
            {
                label = Description?.GetText(defaultKey);
            }

            return label;
        }

        #endregion
    }
}
