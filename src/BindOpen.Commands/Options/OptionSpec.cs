using BindOpen.Framework.MetaData.Elements;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// This class represents an option specification.
    /// </summary>
    public class OptionSpec : ScalarElementSpec, IOptionSpec
    {
        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the OptionSpec class.
        /// </summary>
        public OptionSpec()
        {
        }

        #endregion

        // -------------------------------------------------------------
        // IOptionSpec Implementation
        // -------------------------------------------------------------

        #region IOptionSpec

        /// <summary>
        /// 
        /// </summary>
        public new List<IOptionSpec> SubSpecs => base.SubSpecs.Cast<IOptionSpec>().ToList();

        #endregion
    }
}
