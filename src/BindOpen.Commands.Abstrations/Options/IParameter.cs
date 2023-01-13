using BindOpen.MetaData.Elements;

namespace BindOpen.Commands.Options
{
    /// <summary>
    /// 
    /// </summary>
    public interface IParameter : IBdoMetaScalar
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetValue();
    }
}