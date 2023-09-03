using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using System;
using System.Collections.Generic;

namespace BindOpen.Labs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOption : IBdoSpec
    {
        List<(IBdoExpression Expression, Action)> Executions { get; set; }
    }
}