using BindOpen.System.Data.Helpers;
using BindOpen.System.Scoping;

namespace BindOpen.Pulp.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHelpGenerator : IBdoScoped
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiCulture"></param>
        /// <returns></returns>
        string GetHelpText(IOptionSet optionSet, string uiCulture = StringHelper.__Star);
    }
}