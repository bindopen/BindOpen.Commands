using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using System;
using System.Collections.Generic;

namespace BindOpen.Commands;

/// <summary>
/// This class represents an option specification.
/// </summary>
public class Option : BdoSchema, IOption
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

    public List<(Predicate<IBdoMetaData> Condition, Action Action)> Executions { get; set; }
}
