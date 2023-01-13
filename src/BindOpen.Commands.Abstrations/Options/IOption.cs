using BindOpen.MetaData.Elements;
using System.Collections.Generic;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOption : IBdoMetaScalarSpec
    {
        /// <summary>
        /// 
        /// </summary>
        new List<IOption> SubSpecs { get; }
    }
}