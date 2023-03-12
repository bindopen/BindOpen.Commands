using BindOpen.Data;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents a option set.
    /// </summary>
    public class ParameterSet : TBdoSet<IParameter>, IParameterSet
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
    }
}
