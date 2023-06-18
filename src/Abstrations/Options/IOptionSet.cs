using BindOpen.System.Data;
using BindOpen.System.Data.Helpers;

namespace BindOpen.Labs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOptionSet :
        ITBdoSet<IOption>, INamed,
        IBdoDescribed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiCulture"></param>
        /// <returns></returns>
        string GetHelpText(string uiCulture = StringHelper.__Star);
    }
}