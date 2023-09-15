using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using System;
using System.Collections.Generic;

namespace BindOpen.Pulp.Commands
{
    /// <summary>
    /// This class represents an option specification.
    /// </summary>
    public class Option : BdoSpec, IOption
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

        public List<(IBdoExpression Expression, Action)> Executions { get; set; }
    }
}
