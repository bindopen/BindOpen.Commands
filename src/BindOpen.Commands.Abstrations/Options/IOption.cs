using BindOpen.Data.Elements;
using System.Collections.Generic;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOption : IScalarElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        new List<IOption> SubSpecs { get; }
    }
}