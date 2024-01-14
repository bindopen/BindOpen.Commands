using BindOpen.Data.Helpers;
using BindOpen.Scoping;

namespace BindOpen.Plus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHelpGenerator : IBdoScoped
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <param name="uiCulture"></param>
        /// <returns></returns>
        string GetHelpText(IOption option, string uiCulture = StringHelper.__Star);
    }
}