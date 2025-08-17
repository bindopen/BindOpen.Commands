using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using System;
using System.Collections.Generic;

namespace BindOpen.Commands;

/// <summary>
/// 
/// </summary>
public interface IOption : IBdoSchema
{
    List<(Predicate<IBdoMetaData> Condition, Action Action)> Executions { get; set; }
}