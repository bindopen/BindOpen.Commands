using BindOpen.Framework.MetaData.Elements;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IParameter : IScalarElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetValue();
    }
}