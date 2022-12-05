using BindOpen.Framework.MetaData.Elements;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option set.
    /// </summary>
    public class Option : ScalarElement, IOption
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Option class.
        /// </summary>
        public Option() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            return GetItem();
        }

        #endregion
    }
}
