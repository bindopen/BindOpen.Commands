using BindOpen.Data.Items;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option set.
    /// </summary>
    public class ParameterSet : TBdoItemSet<IParameter>, IParameterSet
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSet class.
        /// </summary>
        public ParameterSet() : base()
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
        public IParameterSet AddOptions(params IParameter[] options)
        {
            return base.Add(options) as IParameterSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IParameter GetOption(string name)
        {
            return Get<IParameter>(name);
        }

        #endregion
    }
}
