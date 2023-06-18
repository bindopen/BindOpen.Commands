using BindOpen.System.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Labs.Commands
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