using BindOpen.Framework.MetaData.Elements;
using System.Collections.Generic;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptionSpec : IScalarElementSpec
    {
        /// <summary>
        /// 
        /// </summary>
        new List<IOptionSpec> SubSpecs { get; }
    }
}