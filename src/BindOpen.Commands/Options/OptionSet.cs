using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option set.
    /// </summary>
    public class OptionSet : TDataItemSet<IOption>, IOptionSet
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSet class.
        /// </summary>
        public OptionSet() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance has the specified option.
        /// </summary>
        /// <param name="name">Name of the option to consider.</param>
        public bool HasOption(string name)
        {
            return HasItem(name);
        }

        /// <summary>
        /// Gets the value of the specified option.
        /// </summary>
        /// <param name="name">Name of the option to consider.</param>
        public object GetOptionValue(string name)
        {
            return Get(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IOptionSet AddOptions(params IOption[] options)
        {
            return base.Add(options) as IOptionSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IOption GetOption(string name)
        {
            return Get<IOption>(name);
        }

        #endregion
    }
}
