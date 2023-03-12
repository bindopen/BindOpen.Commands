using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOption : IBdoSpec
    {
        /// <summary>
        /// 
        /// </summary>
        new List<IOption> SubSpecs { get; }
    }
}