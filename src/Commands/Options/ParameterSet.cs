using BindOpen.System.Data;

namespace BindOpen.Labs.Commands
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
