using BindOpen.Data.Elements;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents an option specification.
    /// </summary>
    public class Option : ScalarElementSpec, IOption
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        public Option()
        {
        }

        #endregion

        // -------------------------------------------------------------
        // IOption Implementation
        // -------------------------------------------------------------

        #region IOption

        /// <summary>
        /// 
        /// </summary>
        public new List<IOption> SubSpecs => base.SubSpecs.Cast<IOption>().ToList();

        #endregion
    }
}
