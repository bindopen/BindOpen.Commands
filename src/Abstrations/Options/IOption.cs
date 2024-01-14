using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;

namespace BindOpen.Plus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOption : IBdoSpec
    {
        List<(Predicate<IBdoMetaData> Condition, Action Action)> Executions { get; set; }
    }
}