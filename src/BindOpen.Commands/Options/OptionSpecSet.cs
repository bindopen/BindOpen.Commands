using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Items;
using System.Linq;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option specification set.
    /// </summary>
    public class OptionSpecSet : TDataItemSet<IOptionSpec>, IOptionSpecSet
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSpecSet class.
        /// </summary>
        public OptionSpecSet() : base()
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
        public new IOptionSpec Get(string key = null)
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
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IOptionSpecSet WithName(string name)
        {
            Name = name;

            return this;
        }

        #endregion

        // ------------------------------------------
        // IDescribedPoco IMPLEMENTATION
        // ------------------------------------------

        #region IDescribedPoco Implementation

        /// <summary>
        /// 
        /// </summary>
        public IDictionaryDataItem Description { get; private set; }

        /// <summary>
        /// Sets the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public IOptionSpecSet WithDescription(string text)
        {
            Description = new DictionaryDataItem();

            return AddDescription("*", text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public IOptionSpecSet WithDescription(IDictionaryDataItem description)
        {
            Description = description;

            return this;
        }

        /// <summary>
        /// Adds the title text.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="text">The text to consider.</param>
        public IOptionSpecSet AddDescription(string key, string text)
        {
            (Description ??= new DictionaryDataItem()).Add(key, text);

            return this;
        }

        /// <summary>
        /// Returns the description label.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="defaultKey">The default variant name to consider.</param>
        public virtual string GetDescriptionText(string key = "*", string defaultKey = "*")
        {
            if (Description == null) return string.Empty;
            string label = Description.GetText(key);
            if (string.IsNullOrEmpty(label))
                label = Description.GetText(defaultKey);
            return label ?? string.Empty;
        }

        #endregion
    }
}
