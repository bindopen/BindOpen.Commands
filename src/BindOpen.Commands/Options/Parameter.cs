using BindOpen.Data.Elements;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option set.
    /// </summary>
    public class Parameter : ScalarElement, IParameter
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Option class.
        /// </summary>
        public Parameter() : base()
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
